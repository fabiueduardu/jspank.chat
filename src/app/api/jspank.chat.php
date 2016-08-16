<?php
$method = $_REQUEST["method"];

switch ($method) {
    case ('new'):
        include 'service/new.php';
        break;
    case ('post'):
        include 'service/post.php';
        break;
    case ('get'):
        include 'service/get.php';
        break;
    case ('friend'):
        include 'service/friend.php';
        break;
    case ('me'):
        include 'service/me.php';
        break;
    default:
        echo json_encode(array ('isvalid'=> false,'message' => 'error, 405 method not allowed'));
}

?>