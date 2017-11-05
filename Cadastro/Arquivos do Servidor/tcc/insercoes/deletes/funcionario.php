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
	
	$sql = "DELETE FROM funcionario
			WHERE Cd_Funcionario = "	. "'" . $_GET["codigo"] . "'";
	
	mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD
	
	$sql = "DELETE FROM func_func
			WHERE Cd_Funcionario = "	. "'" . $_GET["codigo"] . "'";
	
	mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD
