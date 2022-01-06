
<?
	header('P3P: CP="NOI CURa ADMa DEVa TAIa OUR DELa BUS IND PHY ONL UNI COM NAV INT DEM PRE"');
	header("Pragma: no-cache");
	header("Cache-Control: no-cache,must-revalidate");
?>
<?php
//Sending non-POST requests may be blocked by the API server!
require("config.php");
require("util.php");
$ccID = $_POST['ccID'];
$result = get_apiserver_minimumfee($ccID);
echo "$result";
?>
