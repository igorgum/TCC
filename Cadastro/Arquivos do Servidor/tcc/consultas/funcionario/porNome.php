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
	
	$sql = "SELECT funcionario.Cd_Funcionario, funcionario.Login, funcionario.Nm_Funcionario, funcionario.Email, funcao.Funcao
			from funcionario
			INNER JOIN func_func ON funcionario.Cd_Funcionario = func_func.Cd_Funcionario INNER JOIN funcao ON
			func_func.Cd_Funcao = funcao.Cd_Funcao
			where funcionario.Nm_Funcionario LIKE " . '"%' . $_GET["nome"] . '%"';
	
	$result = mysqli_query($mysqli, $sql); //EXECUTA O COMANDO NO BD

	
	while($linha = mysqli_fetch_object($result))
	{
		echo utf8_encode($linha -> Cd_Funcionario); //O UTF É PRA USAR ACENTOS SEM PRECISAR DO HEADER
		echo '|';
		echo utf8_encode($linha -> Login);
		echo '|';
		echo utf8_encode($linha -> Nm_Funcionario);
		echo '|';
		echo utf8_encode($linha -> Email);
		echo '|';
		echo utf8_encode($linha -> Funcao);
		echo '|';
	}
	