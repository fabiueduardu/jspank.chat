<?php
include 'jspank.partial.config.php';
include 'jspank.partial.domain.php';

if(!AppService::validateNew())
{
    echo json_encode(array ('isvalid'=>false,'message' => AppService::message['error']));
    exit;
}

$userid = isset($_REQUEST['userid']) ? $_REQUEST['userid'] : AppService::newguid();
$dbid = AppService::newguid();
$username = $_REQUEST['username'];
$description = isset($_REQUEST['description']) ? $_REQUEST['description'] : $username;

$AppService = new AppService($dbid);
$db_result = $AppService -> init();

$SettingService = new SettingService($dbid);
$db_result = $SettingService -> add(1 , 'version', '1');
$db_result = $SettingService -> add(2 , 'userid', $userid);
$db_result = $SettingService -> add(3 , 'username', $username);
$db_result = $SettingService -> add(4 , 'description', $description);

$UserService = new UserService($dbid);
$db_result = $UserService -> add($username);

$PostService = new PostService($dbid);
$db_result = $PostService -> add($username, AppService::message['success']);

$result = array ('dbid'=> $dbid, 'isvalid'=>$db_result,'message' => AppService::message['success']);
echo json_encode($result);

?>