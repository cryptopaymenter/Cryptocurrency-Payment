
<?php
//Sending non-POST requests may be blocked by the API server!



//mywallet index means ccID (currency type)
// !!! The administrator's wallet address should be written here. !!!
$mywallet[0] = "your tron wallet address"; //Tron
//$mywallet[1] = ""; //New currencies to be supported in the future
//$mywallet[2] = "";

//$wallet_privatekey[0] = "your tron wallet private key"; //This is used to refund process.

$api_host = "apidigitalcurrency.com"; //"apidigitalcurrency.com" is api server address
$api_url = "/jsonapi";

static $maxcount_ccID = 1;
$min_wallet_addr_length = 8;
$max_wallet_addr_length = 512;
?>
