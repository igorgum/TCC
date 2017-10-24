<?php
	//pasta que vc deseja salvar o arquivo
	$uploaddir = 'uploads/';
	
	//cria pasta "uploads/"
	if (!file_exists('uploads/')) {
		mkdir('uploads/', 0777, true);
	}
	
	//monta os parametros
	$uploadfile = $uploaddir . $_FILES['arquivo']['name'];
	
	//move arquivo recebido via superclasse pra dentro da pasta local
	if(move_uploaded_file($_FILES['arquivo']['tmp_name'], $uploadfile)){
		echo 'consegui';
	}else{
		echo 'ops';
	}
?>

