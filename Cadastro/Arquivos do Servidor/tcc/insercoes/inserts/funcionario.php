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
	
	$sql = "INSERT INTO funcionario (Cd_Funcionario, Login, Nm_Funcionario, Email)
			VALUES (" 		. "'" .	$_POST["VARCd_Funcionario"]	. "'".  ","
							. "'" . $_POST["VARLogin"] 			. "'".	","
							. "'" . $_POST["VARNm_Funcionario"]	. "'".	","
							. "'" . $_POST["VAREmail"] 			. "'".
			")";
	
	mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD
	
	$sql = "INSERT INTO func_func (Cd_Funcionario, Cd_Funcao)
			VALUES (" 		. "'" .	$_POST["VARCd_Funcionario"]	. "'".	","
							. "'" . $_POST["VARCdFuncao"] 		. "'".
			")";
			
	mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD