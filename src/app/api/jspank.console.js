var module = angular.module('app', []);
var api_url = {
    me: 'jspank.me.php',
    new: 'jspank.new.php',
    post: 'jspank.post.php',
    get: 'jspank.get.php',
    console: 'jspank.console.htm'
};

module.controller('chat', ['$scope', '$http', '$httpParamSerializer', '$location', function ($scope, $http, $httpParamSerializer, $location) {
    $scope.current_host = location.href.substring(0, location.href.lastIndexOf('/') + 1);
    $scope.current_host_console = $scope.current_host + api_url.console;

    //### me
    $scope.me_feed = $scope.current_host + api_url.me;//feed of me chats [dbs:{dbid:?, description:?}]

    //### new
    $scope.new_username = '';//user name of new chat
    $scope.new_host = $scope.current_host;//host of new chat

    //### post
    $scope.post_dbid = '';//current post path
    $scope.post_host = '';//current post path    
    $scope.post_username = '';//current username
    $scope.post_username_add = '';//current usernames to add
    $scope.post_username_remove = '';//current usernames to remove
    $scope.post_post = '';//current post
    $scope.post_getseconds = '';//current seconds to get posts
    $scope.post_getseconds_count = 0;//current count gets

    //### all
    $scope.me_chats = [];
    $scope.me_chats_posts = [];

    $scope.submitNew = function (form) {
        form.isvalid = false;
        form.message = null;

        if (form.$invalid) return;

        var data = { username: $scope.new_username };
        var api = $scope.new_host + api_url.new;

        $http.post(
            api
            , $httpParamSerializer(data)
            , { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (result) {
                form.isvalid = result.data.isvalid;
                if (result.data.isvalid === false)
                    form.message = result.data.message;
                else {
                    $scope.post_host = $scope.new_host;
                    $scope.post_dbid = result.data.dbid;
                    $scope.post_username = $scope.new_username;
                }
            });
    }

    $scope.submitMe = function (form) {
        form.isvalid = false;
        form.message = null;

        if (form.$invalid) return;

        var data = {};
        var api = $scope.me_feed;

        $http.post(
            api
            , $httpParamSerializer(data)
            , { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (result) {
                form.isvalid = result.data.isvalid;

                if (result.data.isvalid === false)
                    form.message = result.data.message;
                else {
                    angular.forEach(result.data.dbs, function (value, item) { $scope.me_chats.unshift(value); });
                }
            });
    }

    $scope.submitPost = function (form) {
        form.isvalid = false;
        form.message = null;

        if (form.$invalid) return;

        var data = {
            username: $scope.post_username
            , username_add: $scope.post_username_add
            , username_remove: $scope.post_username_remove
            , post: $scope.post_post
            , dbid: $scope.post_dbid
        };
        var api = $scope.post_host + api_url.post;

        $http.post(
            api
            , $httpParamSerializer(data)
            , { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (result) {
                form.isvalid = result.data.isvalid;

                if (result.data.isvalid === false)
                    form.message = result.data.message;
            });
    }

    var getsetInterval = null;
    $scope.submitGet = function (form) {
        form.isvalid = false;
        form.message = null;

        var api = $scope.post_host + api_url.get;
        var _get = function () {
            var data = { username: $scope.post_username, dbid: $scope.post_dbid, postid: ($scope.me_chats_posts[0] || { postid: 0 }).postid };
            $http.post(
                api
                , $httpParamSerializer(data)
                , { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(function (result) {
                    form.isvalid = result.data.isvalid;

                    if (result.data.isvalid === false)
                        form.message = result.data.message;
                    else {
                        angular.forEach(result.data.posts, function (value, item) { $scope.me_chats_posts.unshift(value); });
                        $scope.post_getseconds_count++;
                    }
                });
        }

        clearInterval(getsetInterval);
        if ($scope.post_dbid && $scope.post_username) {
            var seconds = parseInt($scope.post_getseconds);
            if ($scope.post_getseconds) {
                var seconds = parseInt($scope.post_getseconds);
                getsetInterval = setInterval(function () { _get() }, (seconds * 1000));
            } else
                _get();
        }
    }

    $scope.submitChat = function (target) {
        $scope.me_chats_posts = [];
        $scope.post_getseconds_count = 0;

        $scope.post_dbid = target.dbid;
        $scope.post_host = target.host;
        $scope.post_username = target.username;
    }

    $scope.submitPreGet = function (form) {
        var this_search = $location.search();
        if (this_search) {
            var target = {
                dbid: this_search.dbid,
                host: this_search.host,
                username: this_search.username,
                description: 'friend'
            };
            $scope.post_dbid = target.dbid;
            $scope.post_host = target.host;
            $scope.post_username = target.username;

            if (target.dbid && target.host && target.username) {
                $scope.submitChat(target);
                $scope.submitGet(form);
                $scope.me_chats.unshift(target);
            }
        }
    };

}]);

