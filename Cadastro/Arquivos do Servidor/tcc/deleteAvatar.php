<?php

	$avatar = $_GET["codigo"].".png";
	
	if (file_exists('uploads/'.$avatar)) {
		unlink ('uploads/'.$avatar);
	}
	
	
?>

