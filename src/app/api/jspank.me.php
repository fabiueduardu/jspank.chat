<?php
include 'jspank.partial.config.php';
include 'jspank.partial.domain.php';

$files = glob('data/*', GLOB_BRACE);
foreach($files as $file) {
     $results[] =  str_replace("data/","",  $file );
}

$result = array ('dbs'=>$results, 'isvalid'=> true, 'message' => AppService::message['success']);
echo json_encode($result);
?>