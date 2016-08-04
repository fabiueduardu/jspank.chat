<?php
include 'jspank.partial.config.php';
include 'jspank.partial.db.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$postid = isset($_REQUEST['postid']) ? $_REQUEST['postid'] : 0;
$forward = isset($_REQUEST['forward']) && $_REQUEST['forward'] == false ? false : true;

if (file_exists('data/'.$dbid.$const_db_extension)) 
   $const_db = DbInstance($dbid.$const_db_extension);

if($const_db == null)
  $result = array ('isvalid' => false, 'message' => $const_message['error_404']);
else 
{
    $db_result_user =  DbExecuteRead($const_db, $queries["user_select_target"] , array(':username'=> $username, ':active' => true));

    if( count($db_result_user ) > 0)
    {
      $db_result = DbExecuteRead($const_db, $queries[$forward ? "post_select_foward" : "post_select_back"] , array(':postid'=> $postid));
      $result = array ('dbid'=> $dbid, 'posts'=> $db_result ,'isvalid' => true, 'message' => $const_message['success']);
    }else {
      $result = array ('isvalid' => false, 'message' => $const_message['error_401']);
    }
}

  echo json_encode($result);

  $const_db = null;
?>