<?php
	$servidor	= 'localhost';  // ENDEREÇO DO SERVIDOR DO BD
	$usuario	= 'root';		// USUÁRIO DO BD
	$senha		= '';			//SENHA DO BD
	$bancoDados	= 'tcc';	//NOME DO BD
	
	$mysqli		= mysqli_connect($servidor, $usuario, $senha, $bancoDados);
	if(mysqli_connect_error())
	{
		die("error");
	}
	
	$sql = "SELECT Funcao FROM Funcao ORDER BY Cd_Funcao";
	
	$result = mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD

	
	while($linha = mysqli_fetch_object($result))
	{
		echo utf8_encode($linha -> Funcao);
		echo ';';
	}
	