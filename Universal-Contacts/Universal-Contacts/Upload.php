<?php

$filename = "default.xml";

$data = file_get_contents($filename);

if( $_POST != Null && $_POST["data"] )
{
	if( strlen($_POST["fileName"].length()) > 0 )
    {
        $filename   = $_POST["fileName"] + ".xml";
    }
    
    $data       = $_POST["data"];
    file_put_contents($filename, $data);
}

print($data);

?>