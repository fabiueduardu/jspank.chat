<?php
include 'jspank.partial.config.php';
include 'jspank.partial.domain.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$username_add = isset($_REQUEST['username_add']) ? $_REQUEST['username_add'] : null;
$username_remove = isset($_REQUEST['username_remove']) ? $_REQUEST['username_remove'] : null;
$post = $_REQUEST['post'];
$result = array ('isvalid' => false, 'message' => AppService::message['error_404']);

if(AppService::has($dbid))
{
    $result = array ('isvalid' => false, 'message' => AppService::message['error_401']);
    
    $UserService = new UserService($dbid);
    $db_user = $UserService -> get($username);
    
    if( count($db_user ) > 0)
    {
        $PostService = new PostService($dbid);
        $UserService = new UserService($dbid);
        
        $db_result = $PostService -> add($username, $post);
        $result = array ('isvalid' => true, 'message' => AppService::message['success']);
        
        if($username_add != null){
            $db_maxpostid = $PostService -> getmaxpostid()[0]['postid'];
                        
            foreach(explode(',',$username_add) as $key => $val){
                $UserService -> add($val , $db_maxpostid );
                $UserService -> update_active($val, true);
            }
        }
        if($username_remove != null){
            foreach(explode(',',$username_remove) as $key => $val){
                $UserService -> update_active($val, false);
            }
        }
    }
}

echo json_encode($result);

?>