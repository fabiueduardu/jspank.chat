var app = angular.module('app', []);
var app_var = {
    http_headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    message: {
        error: 'there was an error, please try again later',
        error_404: 'error, 404 not found',
        success: 'success',
        get: function (data) { return (data.status === 404 ? app_var.message.error_404 : app_var.message.error) }
    },
    serviceMethod: {
        toNew: 'new',
        toGet: 'get',
        toPost: 'post',
        toFriend: 'friend'
    }
};

app.controller('appCtrl', function ($scope) {
    $scope.post_getting = false;
    $scope.submitTarget = function (target) {
        $scope.dbid = target.dbid;
        $scope.host = target.host;
        $scope.username = target.username;
    }
});

//### NEW
app.directive('new', function () {
    var directive = {
        scope: true,
        templateUrl: 'template/new.htm',
        controller: ['$scope', '$http', '$httpParamSerializer', function ($scope, $http, $httpParamSerializer) {
            $scope.host = null;
            $scope.username = null;

            $scope.submit = function (form) {
                form.isvalid = false;
                if (form.$invalid) return;

                var host = $scope.host;
                var data = { method: app_var.serviceMethod.toNew, username: $scope.username };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;
                }, function (data) {
                    form.message = app_var.message.get(data);
                });

            }

        }]
    }

    return directive;
});

//### ME
app.directive('me', function () {
    var directive = {
        scope: true,
        templateUrl: 'template/me.htm',
        controller: ['$scope', '$http', function ($scope, $http) {
            $scope.contacts = [];
            $scope.host = '/chat/jspank.chat.php?method=me';

            $scope.submit = function (form) {
                form.isvalid = false;
                if (form.$invalid) return;

                var host = $scope.host;
                $http.get(host).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;
                    if (form.isvalid === true)
                        angular.forEach(result.data.contacts, function (value, item) { $scope.contacts.unshift(value); });

                }, function (data) {
                    form.message = app_var.message.get(data);
                });

            }

        }]
    }

    return directive;
});

//### POST
app.directive('post', function () {
    var directive = {
        scope: true,
        templateUrl: 'template/post.htm',
        controller: ['$scope', '$http', '$httpParamSerializer', function ($scope, $http, $httpParamSerializer) {

            $scope.submitFriend = function (form) {
                var host = $scope.host;
                var data = {
                    method: app_var.serviceMethod.toFriend,
                    dbid: $scope.dbid,
                    username: $scope.username,
                    username_add: $scope.username_add,
                    post: $scope.post
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;

                    if (form.isvalid === true) $scope.username_add = null;
                }, function (data) {
                    form.message = app_var.message.get(data);
                });
            }

            $scope.submit = function (form) {
                form.isvalid = false;
                if (form.$invalid) return;

                var host = $scope.host;
                var data = {
                    method: app_var.serviceMethod.toPost,
                    dbid: $scope.dbid,
                    username: $scope.username,
                    post: $scope.post
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;
                }, function (data) {
                    form.message = app_var.message.get(data);
                });
            }

        }]
    }

    return directive;
});

//### GET
app.directive('get', function () {
    var directive = {
        scope: true,
        templateUrl: 'template/get.htm',
        controller: ['$scope', '$http', '$httpParamSerializer', function ($scope, $http, $httpParamSerializer) {
            $scope.posts = [];
            $scope.users = [];
            $scope.getting_seconds = 0;
            $scope.getting_seconds_count = 0;

            var gettingInterval = null;

            $scope.$watch('post_getting', function () {
                if ($scope.getting_seconds === 0) return;

                clearInterval(gettingInterval);
                if ($scope.post_getting)
                    gettingInterval = setInterval(function () { $scope.getting(); }, 1000 * $scope.getting_seconds);

            });

            $scope.submitFriendToRemove = function (form, username) {
                var host = $scope.host;
                var data = {
                    method: app_var.serviceMethod.toFriend,
                    dbid: $scope.dbid,
                    username: $scope.username,
                    username_remove: username,
                    post: $scope.post
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;

                    if (form.isvalid === true) $scope.username_add = null;
                }, function (data) {
                    form.message = app_var.message.get(data);
                });
            }

            $scope.getting = function () {
                var host = $scope.host;
                var data = {
                    method: app_var.serviceMethod.toGet,
                    dbid: $scope.dbid,
                    username: $scope.username,
                    postid: ($scope.posts[0] || { postid: 0 }).postid
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    $scope.isvalid = result.data.isvalid;
                    if (result.data.isvalid === false)
                        $scope.message = result.data.message;
                    else {
                        $scope.getting_seconds_count++;
                        $scope.users = result.data.users;
                        angular.forEach(result.data.posts, function (value, item) { $scope.posts.unshift(value); });
                    }
                });
            }

        }]
    }

    return directive;
});

//### ALL
//### ALL - VALIDATE
app.directive("required", function () {
    return {
        scope: { key: '@' },
        restrict: "E",
        templateUrl: 'template/required.htm',
        require: "^form",
        link: function (scope, element, attrs, form) {
            scope.form = form;
        }
    }
});

//### ALL - VALIDATE
app.directive("alert", function () {
    return {
        scope: { key: '@' },
        restrict: "E",
        templateUrl: 'template/alert.htm',
        require: "^form",
        link: function (scope, element, attrs, form) {
            scope.form = form;
        }
    }
})