
function GET(url, metodo) {
    var xhr = new XMLHttpRequest();
    xhr.open("get", url, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            metodo(xhr.responseText);
        }
    }; xhr.send();
}

function POST(url, metodo) {
    var xhr = new XMLHttpRequest();
    xhr.open("post", url, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            metodo(xhr.responseText);
        }
    }; xhr.send();
}


/*OPERACIONES CON CONTROLES*/
function cargarCombo(lista, nomCombo, etiqueta) {
    var combo = document.getElementById(nomCombo);
    var campos;
    var contenido = "<option value='' disabled selected>";
    contenido += etiqueta;
    contenido += "</option>";
    for (i = 0; i < lista.length; i++) {
        campos = lista[i].split("|");
        contenido += "<option value='" + campos[0] + "'>";
        contenido += campos[1];
        contenido += "</option>";
    }
    combo.innerHTML = contenido;
}

function cargarCombo(lista, nomCombo, etiqueta, val_defecto) {
    var combo = document.getElementById(nomCombo);
    var campos, seleccionado = "";
    //var contenido = "<option value='' disabled selected>";
    var contenido = "<option value='' disabled selected>";
    contenido += etiqueta;
    contenido += "</option>";
    for (i = 0; i < lista.length; i++) {
        campos = lista[i].split("|");
        if (val_defecto == campos[0]) {
            seleccionado = " selected"
        }
        contenido += "<option value='" + campos[0] + "'";
        contenido += ">";
        contenido += campos[1];
        contenido += "</option>";
    }
    combo.innerHTML = contenido;
}

function validarCampo(Tex, tipo) {
    var Texto = document.getElementById(Tex);
    var valor = Texto.value;
    if (Texto != null && valor != null) {
        if (tipo == 0) Texto.value = Texto.value.trim();
        if (Texto.value.length == 0) {
            return false;
        } return true;
    }
    return false;
}


function formatoNroRegistro(n) {
    var rpta;
    if (n < 10) return "000" + n;
    if (n < 100) return "00" + n;
    if (n < 1000) return "0" + n;
    return n;
}

function restarHoras() {
    inicio = document.getElementById("inicio").value;
    fin = document.getElementById("fin").value;
    inicioMinutos = parseInt(inicio.substr(3, 2));
    inicioHoras = parseInt(inicio.substr(0, 2));
    finMinutos = parseInt(fin.substr(3, 2));
    finHoras = parseInt(fin.substr(0, 2));
    transcurridoMinutos = finMinutos - inicioMinutos;
    transcurridoHoras = finHoras - inicioHoras;
    if (transcurridoMinutos < 0) {
        transcurridoHoras--;
        transcurridoMinutos = 60 + transcurridoMinutos;
    }
    horas = transcurridoHoras.toString();
    minutos = transcurridoMinutos.toString();
    if (horas.length < 2) {
        horas = "0" + horas;
    }
    if (horas.length < 2) {
        horas = "0" + horas;
    }
    document.getElementById("resta").value = horas + ":" + minutos;
}

function FormatoFecha_Hora(fecha) {
    var dia, mes, anio, hora, min;
    dia = fecha.getDate();
    mes = fecha.getMonth() + 1;
    anio = fecha.getFullYear();
    hora = fecha.getHours();
    min = fecha.getMinutes();
    if (dia < 10) dia = "0" + dia;
    if (mes < 10) mes = "0" + mes;
    if (hora < 10) hora = "0" + hora;
    if (min < 10) min = "0" + min;
    var rpta = "";
    rpta = dia + "/" + mes + "/" + anio + " " + hora + ":" + min;
    return rpta;
}

function tiempoT(fecha_inicio) {
    var inicioMsec = fecha_inicio.getMilliseconds();
    var ahora = new Date();
    var tiempo = (ahora.getTime() - fecha_inicio) / 1000;
    document.write(tiempo);
}

/*GESTION MENU*/
function crearMenu(nro) {
    var contenido = "";
    var c = " class='seleccionado'";
    m1 = ""; m2 = ""; m3 = "";

    if (nro == 1) { m1 = c; }
    if (nro == 2) { m2 = c; }
    if (nro == 3) { m3 = c; }

    contenido = "<nav class='menu'>";
    contenido += "<a href='#'" + m1 + " onclick='ticket()'>Inicio</a>";
    contenido += "<a href='#'" + m2 + " onclick='nuevo()'>Abrir un Nuevo Ticket</a>";
    contenido += "<a href='#'" + m3 + " onclick='listar()'>Ver Estado de un Ticket</a>";
    contenido += "<a href='#' onclick='salir()'>Cerrar Sesion</a>";
    contenido += "</nav>";
    var div = document.getElementById("divMenu");
    div.innerHTML = contenido;
}

function salir() {
    sessionStorage.clear();
    window.location = "Login";
}