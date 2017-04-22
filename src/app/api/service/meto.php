<?php
include 'partial.config.php';
include 'partial.domain.php';

$dbid = $_REQUEST['dbid'];
$userid = $_REQUEST['userid'];
$result = array ('isvalid' => false, 'message' => AppService::getMessage('error_404' , $language));

if(AppService::has($dbid))
{
    $result = array ('isvalid' => false, 'message' => AppService::getMessage('error_401' , $language));
    
    $UserService = new UserService($dbid);
    $db_user = $UserService -> get(null , true , $userid);
    
    if(!empty($db_user))
    {
        $UserService = new UserService($dbid);        
        $result = array ('dbid'=> $dbid,'isvalid' => true, 'message' => AppService::getMessage('success' , $language), 'username' => $db_user[0]['username'] );
        
     }
}

echo json_encode($result);

?>