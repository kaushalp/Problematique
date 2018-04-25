<?php
    for ($x = 0; $x <= 1900; $x++)
    {
        $filePath="Big.txt";
        $handle = fopen($filePath, "r") or die("Unable to open file!");
	    //$result = file_get_contents('big.txt');
	    $fileContents = fread($handle, filesize($filePath));
    }
    if(!empty($fileContents)) {
        echo "<pre>".$fileContents."</pre>";
	echo "Testing changes";
    }
?>
