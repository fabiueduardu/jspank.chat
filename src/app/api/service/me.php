<?php
include 'partial.config.php';
include 'partial.domain.php';

$files = glob('data/*', GLOB_BRACE);
$results = [];
$index = 1;
foreach($files as $file) {
$results[] =  array('dbid'=> str_replace("data/","",  $file ) , 'name'=> 'Contact '.$index ,'username' => 'me', 'host' => '//'.$_SERVER['HTTP_HOST'].'/chat/jspank.chat.php');
$index++;

if( $index>5) break;
}

$result = array ('contacts'=>$results, 'isvalid'=> true, 'message' => AppService::message['success']);
echo json_encode($result);


?>