
<?
	header('P3P: CP="NOI CURa ADMa DEVa TAIa OUR DELa BUS IND PHY ONL UNI COM NAV INT DEM PRE"');
	header("Pragma: no-cache");
	header("Cache-Control: no-cache,must-revalidate");
?>
<?php
//Sending non-POST requests may be blocked by the API server!

//The end user must call request_payment.php first and check the successful result before calling this php.
require("config.php");
require("util.php");

global $reqip;
$ccID = $_POST['ccID'];
$amount = $_POST['amount'] * 1000000;
$paymentPS = $_POST['payment_password'];
$txID = $_POST['txID'];

//Be sure to filter below if() contents
if($ccID >= $maxcount_ccID) { exit("undefined currency id error"); } //Be sure to filter the ccID to avoid being blocked by the API server
if(!isset($ccID)) { exit("null currency id error"); }
if(!isset($amount) || $amount == 0) { exit("amount error"); }
if(strlen($reqip) < 7 || strlen($reqip) > 45) { exit("fail to identify ip error"); }
if (strlen($paymentPS) > 0 && !ctype_alnum($paymentPS)) { exit("Special characters cannot be used in the payment password. (error)"); }
if (strlen($paymentPS) > 16) { exit("Too long passworde error"); }
if (strlen($txID) < $min_wallet_addr_length || strlen($txID) > $max_wallet_addr_length || !ctype_alnum($txID)) { exit("transaction check error"); }

$cwaddr = $mywallet[$ccID];

$request_body = '{';
$request_body .= "\"reqip\":\"$reqip\",";
$request_body .= "\"ccID\":{$ccID},";
$request_body .= "\"amount\":{$amount},";
$request_body .= "\"cwaddr\":\"{$cwaddr}\",";
$request_body .= "\"paymentPS\":\"{$paymentPS}\",";
$request_body .= "\"txID\":\"{$txID}\"";
$request_body .= "}";

$result = post_url_fsockopen($api_host, 443, $api_url, "startpayment", $request_body);
if (strpos(strtolower($result), "success") === false) { exit("fail to payment error<br>".$result); }

echo "success";

//The end user made a successful payment. Do whatever you want afterward
?>
