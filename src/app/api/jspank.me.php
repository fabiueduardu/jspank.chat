<?php
include 'jspank.partial.config.php';

$files = glob('data/*.{db}', GLOB_BRACE);
foreach($files as $file) {
     $results[] =  str_replace("data/","",str_replace( ".db", "" ,$file));
}

$result = array ('dbs'=>$results, 'isvalid'=>true, 'message' => $const_message['success']);
echo json_encode($result);
?>