<?php
ini_set('display_errors','1');
date_default_timezone_set('UTC');
set_error_handler('error_handler');

function error_handler($errNo, $errStr, $errFile, $errLine) {
        $result = array ('isvalid'=> false, 'message' => "$errStr in $errFile on line $errLine");
        echo json_encode($result);
        die();
}

?>