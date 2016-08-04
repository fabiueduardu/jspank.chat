    var module = angular.module('app', []);
    var api_url = {
            me: 'jspank.me.php',
            new: 'jspank.new.php'
    };
    
    module.controller('new', ['$scope','$http','$httpParamSerializer', function($scope,$http,$httpParamSerializer) {

        $scope.submit = function (form) {
            $scope.isvalid  = false;
            if (form.$invalid)  return;

             var data = { username: $scope.username };           
             var api = $scope.host +  api_url.new;
             
             $http.post(
                 api  
                ,$httpParamSerializer(data) 
                ,{ headers: {'Content-Type': 'application/x-www-form-urlencoded'}}).then(function (result) {
                    $scope.isvalid  =result.data.isvalid; 
                    if (result.data.isvalid === false)
                        $scope.message =result.data.message; 
             });                 
          }
    }]);

    module.controller('me', ['$scope','$http','$httpParamSerializer', function($scope,$http,$httpParamSerializer) {

        $scope.chat = function (data) {
        alert(data);
        
        }
        $scope.submit = function (form) {
            $scope.isvalid  = false;
            if (form.$invalid)  return;

             var data = {  };           
             var api = $scope.host_myconversations;
             
             $http.post(
                 api  
                ,$httpParamSerializer(data) 
                ,{ headers: {'Content-Type': 'application/x-www-form-urlencoded'}}).then(function (result) {
                    $scope.isvalid  =result.data.isvalid;
                    $scope.items = [];
                    
                    if (result.data.isvalid === false)
                        $scope.message =result.data.message;
                    else
                        $scope.items =  result.data.dbs;
             });                 
          }
    }]);    


    module.controller('post', ['$scope','$http','$httpParamSerializer', function($scope,$http,$httpParamSerializer) {
        $scope.submit = function (form) {
            alert('send')
            $scope.isvalid  = false;
            if (form.$invalid)  return;
            
          }
    }]);        