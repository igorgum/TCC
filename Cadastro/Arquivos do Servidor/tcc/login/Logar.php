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
	
	/*
	$sql = "SELECT funcionario.Login, funcionario.Senha, funcionario.NmFuncionario
			FROM funcionario
			WHERE funcionario.Login = " . "'". $_GET["login"]."'"
		  ." AND funcionario.Senha = "   . "'". $_GET["senha"]."'";
	*/
	
	$sql = "SELECT funcionario.login, funcionario.Senha, funcionario.Nm_Funcionario FROM funcionario WHERE funcionario.Login = "
			."'".$_GET["login"]."'"
			." AND funcionario.Senha = " . "'".$_GET["senha"]."'";
	
	$result = mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD


	$linha = mysqli_fetch_object($result);
	if ($linha != null){
			echo utf8_encode($linha -> Nm_Funcionario);
	}else{
		echo '|';
	}