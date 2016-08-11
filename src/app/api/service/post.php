<?php
include 'partial.config.php';
include 'partial.domain.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
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
        $result = array ('dbid'=> $dbid,'isvalid' => true, 'message' => AppService::message['success']);
        
    }
}

echo json_encode($result);

?>