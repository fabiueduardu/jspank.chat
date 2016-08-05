<?php
include 'jspank.partial.config.php';
include 'jspank.partial.domain.php';

$files = glob('data/*', GLOB_BRACE);
$results = [];
$index = 1;
foreach($files as $file) {
     $results[] =  array('dbid'=> str_replace("data/","",  $file ) , 'description'=> 'Chat '.$index ,'username' => 'me', 'host' => 'http://localhost/chat/');
     $index++;
}

$result = array ('dbs'=>$results, 'isvalid'=> true, 'message' => AppService::message['success']);
echo json_encode($result);
?>