var app = angular.module('app', []);
var app_var = {
    version: 0.1,
    http_headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
    message: {
        messages: null,
        get: function (data) {
            if (!app_var.message.messages)
                return 'config, error';

            switch (data.status) {
                case 404:
                    return app_var.message.messages.error_404;
                case 500:
                    return app_var.message.messages.error_500;
                default:
                    return app_var.message.messages.error;
            }
        }
    },
    serviceMethod: {
        toNew: 'new',
        toGet: 'get',
        toPost: 'post',
        toFriend: 'friend'
    },
    hashCode: function (str) {
        var hash = 0;
        for (var i = 0; i < str.length; i++) {
            hash = str.charCodeAt(i) + ((hash << 5) - hash);
        }
        return hash;
    },
    color: function (value) {
        var c = (value & 0x00FFFFFF)
            .toString(16)
            .toUpperCase();

        return "00000".substring(0, 6 - c.length) + c;
    },
    getColor: function (value) {
        return app_var.color(app_var.hashCode(value));
    }
};



//### INIT GLOBAL
app.controller('appCtrl', function ($scope, $http, $location) {
    $scope.contacts = [];
    $scope.post_getting = false;

    $http.get('config.json').then(function (result) {
        var _host = location.protocol + '//' + location.host + '/';
        $scope.new_host = result.data.apiService.replace('{host}', _host);
        app_var.message.messages = result.data.messages;

    }, function (result) {
        $scope.message = app_var.message.get(result);
    });

    $scope.submitTarget = function (target) {
        $scope.post_dbid = target.dbid;
        $scope.post_host = target.host;
        $scope.post_username = target.username;
        $scope.post_name = target.name;

        setTimeout(function () { document.getElementById('get_posts').click(); }, 1000);

    }

    $scope.toClipBoard = function (target) {
        window.prompt("Copy to clipboard: Ctrl+C, Enter", $scope.post_urlshare);
    }

    $scope.getStyle = function (value) {
        return { 'background': '#' + app_var.color(app_var.hashCode(value)), 'color': '#FFF' };
    }

    var search = $location.search();

    if (search.dbid) {
        var target = { name: search.name, dbid: search.dbid, host: search.host, username: search.username };
        $scope.contacts.unshift(target);
        $scope.submitTarget(target);
    }
});

//### NEW
app.directive('new', function () {
    var directive = {
        scope: true,
        templateUrl: 'contents/templates/new.htm?vs=' + app_var.version,
        controller: ['$scope', '$http', '$httpParamSerializer', function ($scope, $http, $httpParamSerializer) {
            $scope.new_username = null;

            $scope.submit = function (form) {
                form.isvalid = false;
                if (form.$invalid) return;

                var host = $scope.new_host;
                var data = { method: app_var.serviceMethod.toNew, username: $scope.new_username };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;

                    var target = { name: 'new > ' + data.username, dbid: result.data.dbid, host: host, username: data.username };
                    $scope.contacts.unshift(target);
                    $scope.submitTarget(target);
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
        templateUrl: 'contents/templates/me.htm?vs=' + app_var.version,
        controller: ['$scope', '$http', function ($scope, $http) {

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
        templateUrl: 'contents/templates/post.htm?vs=' + app_var.version,
        controller: ['$scope', '$http', '$httpParamSerializer', function ($scope, $http, $httpParamSerializer) {

            $scope.submitFriend = function (form) {
                var host = $scope.post_host;
                var data = {
                    method: app_var.serviceMethod.toFriend,
                    dbid: $scope.post_dbid,
                    username: $scope.post_username,
                    username_add: $scope.post_username_add,
                    post: $scope.post_post
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;

                    if (form.isvalid == true) $scope.post_username_add = null;
                }, function (data) {
                    form.message = app_var.message.get(data);
                });
            }

            $scope.submit = function (form) {
                form.isvalid = false;
                if (form.$invalid) return;

                var host = $scope.post_host;
                var data = {
                    method: app_var.serviceMethod.toPost,
                    dbid: $scope.post_dbid,
                    username: $scope.post_username,
                    post: $scope.post_post
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;

                    if (form.isvalid == true) {
                        $scope.post_post = null;
                        form.message = null;
                    }
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
        templateUrl: 'contents/templates/get.htm?vs=' + app_var.version,
        controller: ['$scope', '$http', '$httpParamSerializer', '$interval', function ($scope, $http, $httpParamSerializer, $interval) {
            $scope.posts = [];
            $scope.users = [];
            $scope.getting_seconds = 0;
            $scope.getting_seconds_count = 0;
            var gettingInterval = null;

            $scope.submitFriendToShare = function (form, username) {
                var post_urlshare = '//' + location.host + '/' + location.pathname + '#?host=' + $scope.post_host + '&dbid=' + $scope.post_dbid + '&username=' + username;
                window.prompt("Copy to clipboard: Ctrl+C, Enter", post_urlshare);
            }

            $scope.submitFriendToRemove = function (form, username) {
                var host = $scope.post_host;
                var data = {
                    method: app_var.serviceMethod.toFriend,
                    dbid: $scope.post_dbid,
                    username: $scope.post_username,
                    username_remove: username
                };

                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;
                }, function (data) {
                    form.message = app_var.message.get(data);
                });
            }

            $scope.submit = function (form) {
                form.isvalid = false;
                $scope.post_getting = !$scope.post_getting;

                clearInterval(gettingInterval);
                if ($scope.post_getting)
                    gettingInterval = setInterval(function () {
                        $scope.getting(form, function (result) {
                            if (!result.data.isvalid) {
                                clearInterval(gettingInterval);
                                $scope.post_getting = false;
                            }
                        });

                    }, 1000 * $scope.getting_seconds);
            }

            $scope.getStyle = function (value) {
                return { 'background': '#' + app_var.color(app_var.hashCode(value)), 'color': '#FFF' };
            }
            $scope.gettingOld = function (form) {
                $scope.getting(form, function (result) {
                    $scope.hasOld = result.data.isvalid == true && result.data.posts.length == 0 ? true : false;
                }, false);
            }

            $scope.getting = function (form, callback, forward) {
                var host = $scope.post_host;
                var data = {
                    method: app_var.serviceMethod.toGet,
                    dbid: $scope.post_dbid,
                    username: $scope.post_username,
                    postid: ($scope.posts[0] || { postid: 0 }).postid,
                    forward: (forward === false ? '0' : '1')
                };

                if (forward === false)
                    data.postid = ($scope.posts[$scope.posts.length - 1] || { postid: 0 }).postid

                if (!data.dbid || !data.username || !host) {
                    form.message = app_var.message.get({ status: 500 });
                    return false;
                }
                $http.post(host, $httpParamSerializer(data), { headers: app_var.http_headers }).then(function (result) {
                    form.isvalid = result.data.isvalid;
                    form.message = result.data.message;

                    if (form.isvalid === true) {
                        form.message = null;
                        $scope.getting_seconds_count++;
                        $scope.users = result.data.users;
                        if (forward == false) {
                            angular.forEach(result.data.posts, function (value, item) { $scope.posts.push(value); });
                        } else
                            angular.forEach(result.data.posts, function (value, item) { $scope.posts.unshift(value); });
                    }

                    callback(result);
                }, function (result) {
                    form.message = app_var.message.get(result);
                    callback(result);
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
        templateUrl: 'contents/templates/required.htm?vs=' + app_var.version,
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
        templateUrl: 'contents/templates/alert.htm?vs=' + app_var.version,
        require: "^form",
        link: function (scope, element, attrs, form) {
            scope.form = form;
        }
    }
});

