<?php
include 'partial.config.php';
include 'partial.domain.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$postid = isset($_REQUEST['postid']) ? $_REQUEST['postid'] : 0;
$forward = isset($_REQUEST['forward']) && $_REQUEST['forward'] == '0' ? false : true;
$result = array ('isvalid' => false, 'message' => AppService::getMessage('error_404' , $language));

if(AppService::has($dbid))
{
    $result = array ('isvalid' => false, 'message' => AppService::getMessage('error_401' , $language));
    
    $UserService = new UserService($dbid);
    $db_user = $UserService -> get($username);
    
    if(!empty($db_user))
    {
        $PostService = new PostService($dbid);
        
        if($postid==0){
            $postid = $PostService -> getmaxpostid()[0]['postid'] - $PostService -> limit;
        }
        $minpostid =  $db_user[0]['postid'];
        
        $db_result = $PostService -> get($postid,  $minpostid, $forward);
        $db_result_user =  $UserService -> getAll();
        $result = array ('dbid'=> $dbid, 'posts'=> $db_result ,'users'=> $db_result_user, 'isvalid' => true, 'message' => AppService::getMessage('success' , $language));
    }
    
}

echo json_encode($result);
?>