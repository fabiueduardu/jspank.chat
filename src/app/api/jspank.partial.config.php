<?php
ini_set('display_errors','1');
date_default_timezone_set('UTC');

$const_db_extension = '.db';
$const_db = null;
$const_message = array(
     'success' => 'success'
     ,'error' => 'there was an error, please try again later'
     ,'error_401' => 'error, 401 unauthorized'
     ,'error_404' => 'error, 404 not found'
     ,'error_500' => 'error, 500 internal server error'
     ,'welcome' => 'Welcome'
);

/*### functions */
function newguid()
{
        mt_srand((double)microtime()*10000);
        $charid = strtoupper(md5(uniqid(rand(), true)));
        $hyphen = chr(45);// "-"
        $uuid = substr($charid, 0, 8).$hyphen
                .substr($charid, 8, 4).$hyphen
                .substr($charid,12, 4).$hyphen
                .substr($charid,16, 4).$hyphen
                .substr($charid,20,12);
        return $uuid;
    }
?>