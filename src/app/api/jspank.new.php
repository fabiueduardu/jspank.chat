<?php
include 'jspank.partial.config.php';
include 'jspank.partial.db.php';

$userid = isset($_REQUEST['userid']) ? $_REQUEST['userid'] : newguid();
$dbid = newguid();
$username = $_REQUEST['username'];
$description = isset($_REQUEST['description']) ? $_REQUEST['description'] : $username;

$const_db = DbInstance($dbid.$const_db_extension);

$db_result =DbExecuteNonQuery($const_db , $queries["setting_create"]);
$db_result =DbExecuteNonQuery($const_db , $queries["setting_insert"] , array(':settingid'=>1 ,':name' => 'version' ,':description' => '1.0'));
$db_result =DbExecuteNonQuery($const_db , $queries["setting_insert"] , array(':settingid'=>2 ,':name' => 'userid' ,':description' => $userid));
$db_result =DbExecuteNonQuery($const_db , $queries["setting_insert"] , array(':settingid'=>3 ,':name' => 'username' ,':description' => $username));
$db_result =DbExecuteNonQuery($const_db , $queries["setting_insert"] , array(':settingid'=>3 ,':name' => 'description' ,':description' => $description));

$db_result =DbExecuteNonQuery($const_db , $queries["user_create"]);
$db_result =DbExecuteNonQuery($const_db , $queries["user_insert"] , array(':username' => $username ));

$db_result =DbExecuteNonQuery($const_db , $queries["post_create"]);
$db_result =DbExecuteNonQuery($const_db , $queries["post_insert"] , array(':post' => $const_message['welcome'],':username' => $username ));

$result = array ('dbid'=> $dbid, 'isvalid'=>$db_result,'message' => $const_message['success']);
echo json_encode($result);

$const_db = null;
?>