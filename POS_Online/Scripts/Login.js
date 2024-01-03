
window.onload = function () {
    sessionStorage.clear();
    var btnAceptar = document.getElementById("btnAceptar");
    btnAceptar.onclick = function () { ValidarUsuario(); }
    var btnCancelar = document.getElementById("btnCancelar");
    btnCancelar.onclick = function () { Cancelar(); }
}

function ValidarUsuario() {
    var url = "validarUsuario/?Usuario=";
    url += document.getElementById("txtUsuario").value;
    url += "&Password=";
    var txtClave = document.getElementById("txtPassword").value;
    txtClave = CryptoJS.MD5(txtClave);
    url += txtClave;
    POST(url, validarLogin);
}

function Cancelar() {
    var txt = document.getElementById("txtUsuario");
    txt.value = "";
    txt = document.getElementById("txtPassword");
    txt.value = "";
}

function validarLogin(rpta) {
    if (rpta != "") {
        rpta = "0|Ricardo Segovia|1|0101|4000101"
        var rptas = rpta.split("|");
        sessionStorage.setItem("NroVendedor", rptas[0]);
        sessionStorage.setItem("NomUsuario", rptas[1])
        sessionStorage.setItem("Perfil", rptas[2]);
        sessionStorage.setItem("Division", rptas[3]);
        sessionStorage.setItem("Cliente", rptas[4]);
        window.location = "menu";
    }
    else {
        alert("Usuario no válido");
    }
}

function saltar(e, t) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 13) {
        if (t == 1) ValidarUsuario();
        else document.getElementById("txtPassword").focus();
    }
}
