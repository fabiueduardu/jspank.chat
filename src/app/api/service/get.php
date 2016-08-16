<?php
include 'partial.config.php';
include 'partial.domain.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$postid = isset($_REQUEST['postid']) ? $_REQUEST['postid'] : 0;
$forward = isset($_REQUEST['forward']) && $_REQUEST['forward'] == false ? false : true;
$result = array ('isvalid' => false, 'message' => AppService::message['error_404']);

if(AppService::has($dbid))
{
    $result = array ('isvalid' => false, 'message' => AppService::message['error_401']);
    
    $UserService = new UserService($dbid);
    $db_user = $UserService -> get($username);
    
    if( count($db_user ) > 0)
    {
        if($postid<$db_user[0]['postid'])
        $postid= $db_user[0]['postid'];
        
        $PostService = new PostService($dbid);
        $db_result = $PostService -> get($postid, $forward);
        $db_result_user =  $UserService -> getAll();
        $result = array ('dbid'=> $dbid, 'posts'=> $db_result ,'users'=> $db_result_user, 'isvalid' => true, 'message' => AppService::message['success']);
    }
    
}

echo json_encode($result);
?>