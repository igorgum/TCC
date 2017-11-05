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
	
	$sql = "UPDATE funcionario
			SET Cd_Funcionario = " 		. "'" .	$_POST["VARCd_Funcionario"]	. "'".
			", Login = " 				. "'" . $_POST["VARLogin"] 			. "'".
			", Nm_Funcionario = " 		. "'" . $_POST["VARNm_Funcionario"]	. "'".
			", Email = "				. "'" . $_POST["VAREmail"] 			. "'".
			"WHERE Cd_Funcionario = "	. "'" . $_POST["X"] 				. "'";
	
	mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD
	
	$sql = "UPDATE func_func
			SET Cd_Funcionario = " 		. "'" .	$_POST["VARCd_Funcionario"]	. "'".
			", Cd_Funcao = "			. "'" . $_POST["VARCdFuncao"] 		. "'".
			"WHERE Cd_Funcionario = "	. "'" . $_POST["X"]					. "'";
			
	mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD
