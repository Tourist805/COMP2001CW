<?php

$handle = fopen("/.../assets/Industries.php", "r");

$csvData = array();

while(($row = fgetcsv($handle, 0, ",")) !== FALSE){
    $csvData[] = $row;
}

echo json_encode($csvData);
