<?php
include 'partial.config.php';
include 'partial.domain.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$post = $_REQUEST['post'];
$result = array ('isvalid' => false, 'message' => AppService::getMessage('error_404' , $language));

if(AppService::has($dbid))
{
    $result = array ('isvalid' => false, 'message' => AppService::getMessage('error_401' , $language));
    
    $UserService = new UserService($dbid);
    $db_user = $UserService -> get($username);
    
     if(!empty($db_user))
    {
        $PostService = new PostService($dbid);
        $UserService = new UserService($dbid);
        
        $db_result = $PostService -> add($username, $post);
        $result = array ('dbid'=> $dbid,'isvalid' => true, 'message' => AppService::getMessage('success' , $language));
        
    }
}

echo json_encode($result);

?>