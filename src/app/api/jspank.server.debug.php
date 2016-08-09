<?php
include 'jspank.partial.config.php';
include 'jspank.partial.domain.php';

echo AppService::validateNew()?"s":"n";
echo '<br>';


if(isset($_REQUEST["teste"])){

 $key = '_jspank_KlIdkhdsl0_';
    $_SESSION[$key] = new DateTime();
}
  

//session_start();,

//$_SESSION["teste"] = 1;
//if(isset($_SESSION["teste"]))
//    echo $_SESSION["teste"];

// $date = new DateTime();
// echo $date->format('Y-m-d H:i:s');
// echo '<br>';
// $date->modify('+10 second');
// echo $date->format('Y-m-d H:i:s');

?>