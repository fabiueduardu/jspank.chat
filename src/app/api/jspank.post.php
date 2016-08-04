<?php
include 'jspank.partial.config.php';
include 'jspank.partial.db.php';

$dbid = $_REQUEST['dbid'];
$username = $_REQUEST['username'];
$username_add = isset($_REQUEST['username_add']) ? $_REQUEST['username_add'] : null;
$username_remove = isset($_REQUEST['username_remove']) ? $_REQUEST['username_remove'] : null;
$post = $_REQUEST['post'];

if (file_exists('data/'.$dbid.$const_db_extension))
    $const_db = DbInstance($dbid.$const_db_extension);

if($const_db == null)
$result = array ('isvalid' => false, 'message' => $const_message['error_404']);
else{
    $db_result_user =  DbExecuteRead($const_db, $queries["user_select_target"] , array(':username'=> $username, ':active' => true));
    
    if( count($db_result_user ) > 0)
    {
        $db_result = DbExecuteNonQuery($const_db, $queries["post_insert"] , array(':post' => $post ,':username' => $username ));
        $result = array ('isvalid' => true, 'message' => $const_message['success']);
        
        if($username_add != null){
            foreach(explode(',',$username_add) as $key => $val){
                DbExecuteNonQuery($const_db , $queries["user_insert"] , array(':username' => $val ));
                DbExecuteNonQuery($const_db , $queries["user_update_active"] , array(':username' => $val ,':active' => true));
            }
        }
       if($username_remove != null){
            foreach(explode(',',$username_remove) as $key => $val){
                DbExecuteNonQuery($const_db , $queries["user_update_active"] , array(':username' => $val ,':active' => false));
            }
        }
    }else {
        $result = array ('isvalid' => false, 'message' => $const_message['error_401']);
    }
}

echo json_encode($result);
$const_db = null;
?>