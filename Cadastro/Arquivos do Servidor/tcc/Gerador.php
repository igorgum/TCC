<?php
$senha=$_GET['email'];
$senhaenc = base64_encode($senha);
$senhaenc = $senhaenc . 'sPr';
$usermail = $_GET['email'];
//alteracao do link
$localIP = getHostByName(getHostName());
$link = "http://$localIP:4000/tcc/formulario.php?cx=$senhaenc";
//fim alteracao do link
// sql stuff
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "tcc";
$autorizado=0;

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

// gerar senha agora lol
function random_str($length, $keyspace = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ')
{
    $str = '';
    $max = mb_strlen($keyspace, '8bit') - 1;
    for ($i = 0; $i < $length; ++$i) {
        $str .= $keyspace[random_int(0, $max)];
    }
    return $str;
}
$old_pass = random_str(8);
$new_pass = hash ('md5' , $old_pass);
// mais sql
$sql = "UPDATE funcionario SET funcionario.Senha = '$new_pass' WHERE funcionario.Email = '$usermail';";

if ($conn->query($sql) === TRUE) {
    echo "inseri;";
	$autorizado = 1;
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

$conn->close();

$to = "$usermail";
$subject = "Confirmação de E-mail";
$txt = "Sua senha gerada é $old_pass" . '             Clique neste link para terminar seu cadastro ' . $link . ' caso tenha recebido multiplos emails apenas o ultimo é valido.';
$headers = "Confirme Seu E-mail!";

require 'PHPMailerAutoload.php';
$mail= new PHPMailer;
$mail->IsSMTP();        // Ativar SMTP
$mail->SMTPDebug = false;       // Debugar: 1 = erros e mensagens, 2 = mensagens apenas
$mail->SMTPAuth = true;     // Autenticação ativada
$mail->SMTPSecure = 'ssl';  // SSL REQUERIDO pelo GMail
$mail->Host = 'smtp.gmail.com'; // SMTP utilizado
$mail->Port = 465;
$mail->Sendmail = 'C:\wamp64\sendmail\sendmail.exe -t';
// optional
// used only when SMTP requires authentication  
$mail->SMTPAuth = true;
$mail->Username = 'solowingphanzer@gmail.com';
$mail->Password = 'Kalango321';

$mail->setFrom('solowingphanzer@gmail.com', 'Sistema de Cadastro');
$mail->addAddress("$usermail", 'Ola');
$mail->Subject  = "$subject";
$mail->Body     = "$txt";
$mail->SMTPOptions = array(
'ssl' => array(
    'verify_peer' => false,
    'verify_peer_name' => false,
    'allow_self_signed' => true
));
if($autorizado==1){ 
if(!$mail->send()) {
  echo 'fail;';
  echo 'Mailer error: ' . $mail->ErrorInfo;
} else {
  echo 'kkeaemen;';
}
$autorizado=0;
}
else{
	echo 'morri;';
}
?>