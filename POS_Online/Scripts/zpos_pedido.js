var Caja, Fecha, Usuario, Soc, OrgVta, Canal, Sec, Vendedor, OfVtas, CondPago, Moneda;
var idFila;
var nModal; // 1 Clientes, 2 Materiales

window.onload = function () {
    idFila = 0;
    cargarDatosCabecera();
    fila_vacia();
    cargarDatosCabecera();
    foco("txtBuscarCliente");
    //prueba();
}

function prueba() {
    var url = "pruebaJSON";
    POST(url, pruebaRpta);
}

function pruebaRpta(rpta) {
    alert(rpta);
}


function cargarDatosCabecera() {
    
    Caja = 1;
    var f = new Date();
    Fecha = f.getDate() + "." + (f.getMonth() + 1) + "." + f.getFullYear();
    Usuario = "ABAP03";
    Soc = 1000; OrgVta = 1000; Canal = 10; Sec = "00";
    Vendedor = "10";
    OfVtas = "0101";
    CondPago = "NT00";
    Moneda = "BOB";

    document.getElementById("txtCaja").value = Caja;
    document.getElementById("txtFecha").value = Fecha;
    document.getElementById("txtUsuario").value = Usuario;
    document.getElementById("txtSociedad").value = Soc;
    document.getElementById("txtOrgVentas").value = OrgVta;
    document.getElementById("txtCanal").value = Canal;
    document.getElementById("txtSector").value = Sec;
    document.getElementById("txtVendedor").value = Vendedor;
    document.getElementById("txtOficinaVentas").value = OfVtas;
    document.getElementById("txtCondPago").value = CondPago;
    document.getElementById("txtGrupoCliente").value = "Z1";
    document.getElementById("txtNomGrupoCliente").value = "Clase A";
    document.getElementById("txtMoneda").value = Moneda;
}

function fila_vacia() {
    if (idFila > 0) {
        var result = validarFila(idFila);
        if (result) {
            var cont = "";
            var txtNroMaterial, lblNomMaterial, txtCentro, txtAlm, lblStock, txtCantidad, lblUM, lblPrecU, lblPrecBruto, lblDesc, txtPorcDesc, lblPrecNeto, lblIVA;
            txtNroMaterial = document.getElementById("txtNroMaterial" + idFila).value;
            lblNomMaterial = document.getElementById("lblNomMaterial" + idFila).innerHTML;
            txtCentro = document.getElementById("txtCentro" + idFila).value;
            txtAlm = document.getElementById("txtAlm" + idFila).value;
            lblStock = document.getElementById("lblStock" + idFila).innerHTML;
            txtCantidad = document.getElementById("txtCantidad" + idFila).value;
            lblUM = document.getElementById("lblUM" + idFila).innerHTML;
            lblPrecU = document.getElementById("lblPrecU" + idFila).innerHTML;
            lblPrecBruto = document.getElementById("lblPrecBruto" + idFila).innerHTML;
            lblDesc = document.getElementById("lblDesc" + idFila).innerHTML;
            txtPorcDesc = document.getElementById("txtPorcDesc" + idFila).value;
            lblPrecNeto = document.getElementById("lblPrecNeto" + idFila).innerHTML;
            lblIVA = document.getElementById("lblIVA" + idFila).innerHTML;
            cont += "<td><a class='icon-minus' href='#";
            cont += idFila;
            //contenido += "' onclick='eliminar_fila()'></a></td>";
            cont += "' onclick='quitarFila(this)'></a></td>";
            cont += "<td><label id='txtNroMaterial";
            cont += idFila;
            cont += "'>";
            cont += txtNroMaterial;
            cont += "</label></td>";   //'
            cont += "<td><label id='lblNomMaterial"
            cont += idFila;
            cont += "'>";
            cont += lblNomMaterial;
            cont += "</label></td>";
            cont += "<td><label id='txtCentro";
            cont += idFila;
            cont += "'>";
            cont += txtCentro;
            cont += "</label></td>";
            cont += "<td><label id='txtAlm";
            cont += idFila;
            cont += "'>";
            cont += txtAlm;
            cont += "</td>";   //'
            cont += "<td class='derecha'><label id='lblStock"
            cont += idFila;
            cont += "'>";
            cont += lblStock;
            cont += "</label></td>";
            cont += "<td class='derecha'><label id='txtCantidad";
            cont += "'>";
            cont += txtCantidad;
            cont += "</label></td>";   //'
            cont += "<td class='centro'><label id='lblUM"
            cont += idFila;
            cont += "'>";
            cont += lblUM;
            cont += "</label></td>";
            cont += "<td class='derecha'><label id='lblPrecU"
            cont += idFila;
            cont += "'>";
            cont += lblPrecU;
            cont += "</label></td>";
            cont += "<td class='derecha'><label id='lblPrecBruto"
            cont += idFila;
            cont += "'>";
            cont += lblPrecBruto;
            cont += "</label></td>";
            cont += "<td class='derecha'><label id='lblDesc"
            cont += idFila;
            cont += "'>";
            cont += lblDesc;
            cont += "</label></td>";
            cont += "<td class='derecha'><label id='txtPorcDesc";
            cont += idFila;
            cont += "'>";
            cont += txtPorcDesc;
            cont += "</label></td>";   //'
            cont += "<td class='derecha'><label id='lblPrecNeto"
            cont += idFila;
            cont += "'>";
            cont += lblPrecNeto;
            cont += "</label></td>";
            cont += "<td class='derecha'><label id='lblIVA"
            cont += idFila;
            cont += "'>";
            cont += lblIVA;
            cont += "</label></td>";

            document.getElementById("f" + idFila).innerHTML = "";
            document.getElementById("f" + idFila).innerHTML = cont;
        }
        else {
            return;
        }
    }
    idFila += 1;
    var fila = idFila + 1;
    var contenido = "";
    contenido = document.getElementById("tbDetalle_Pedido").innerHTML;
    document.getElementById("tbDetalle_Pedido").innerHTML = "";
    contenido += "<tr id='f";
    contenido += idFila;
    contenido += "'>";
    contenido += "<td><a class='icon-plus' href='#"; //contenido += fila;
    contenido += "' onclick='fila_vacia()'></a></td>";
    contenido += "<td><input type='search' autocomplete='on' class='caja' id='txtNroMaterial";
    contenido += idFila;
    contenido += "' onKeyDown='abrirPopUp(event,2)' onkeypress='return validaNumero(event)'/></td>";   //'
    contenido += "<td><label id='lblNomMaterial"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td><input class='caja bloqueada' readonly='readonly' id='txtCentro";
    contenido += idFila;
    contenido += "'/></td>";
    contenido += "<td><input class='caja bloqueada' readonly='readonly' id='txtAlm";
    contenido += idFila;
    contenido += "'/></td>";   //'
    contenido += "<td class='derecha'><label id='lblStock"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td><input class='caja derecha' id='txtCantidad";
    contenido += idFila;
    //contenido += "' value=0 onblur='compruebaValidoEntero(this)'/></td>";   //'
    contenido += "' value=0 onkeypress='return validaNumero(event)'/></td>";   //'
    //contenido += "' value=0 /></td>";   //'
    contenido += "<td><label id='lblUM"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td class='derecha'><label id='lblPrecU"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td class='derecha'><label id='lblPrecBruto"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td><label id='lblDesc"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td class='derecha'><input class='caja' id='txtPorcDesc";
    contenido += idFila;
    contenido += "' value=0 onkeypress='return validaNumero(event)'/></td>";   //'
    contenido += "<td><label id='lblPrecNeto"
    contenido += idFila;
    contenido += "'></label></td>";
    contenido += "<td class='derecha'><label id='lblIVA"
    contenido += idFila;
    contenido += "'>0</label></td>";
    contenido += "</tr>";
    document.getElementById("tbDetalle_Pedido").innerHTML = contenido;
    foco("txtNroMaterial" + idFila);
}

function agregarFila() {

}

function quitarFila(a) {
    var tabla = a.parentNode.parentNode.parentNode;
    tabla.removeChild(a.parentNode.parentNode);
}

function validarFila(idFila) {
    var txtNroMaterial, txtCantidad;
    txtNroMaterial = "txtNroMaterial" + idFila;
    txtCantidad = "txtCantidad" + idFila;
    if (validarCampo(txtNroMaterial, 0) == false) {
        alert("Error en Material");
        document.getElementById(txtNroMaterial).focus();
        return false;
    }
    if (validarCampo(txtCantidad, 0) == false) {
        alert("Error en Cantidad");
        document.getElementById(txtCantidad).focus();
        return false;
    }
    return true;
}

function validarCantidad(nomTxt,MsgError) {
    var txt = document.getElementById(nomTxt);
    var val = txt.value;
    if (isNaN(val) == true) {
        txt.focus();
    }
}

function validarEntero(valor) {
    valor = parseInt(valor)
    if (isNaN(valor)) {
        return ""
    } else {
        return valor
    }
}

function compruebaValidoEntero(txt) {
    var enteroValidado = validarEntero(txt.value);
    if (enteroValidado == "") {
        alert("Debe escribir un entero!");
        txt.select();
        txt.focus();
    } else
        txt.value = enteroValidado;
}

function abrirPopUp(e, numModal) {
    if (numModal == 1) {
    }
    if (numModal == 2) {
        abrirPantallaBusqArticulo(e);
    }
}

function abrirPantallaBusqArticulo(e) {
    if (e.key == "F4") {
        mostrarModal("divBusqMateriales");
        foco("GV_ZZ_DESCRIPCION");
    }
    if (e.key == "Enter") {
        alert("Verificar si el codigo ingresado es válido");
        mostrarModal("divBusqMateriales");
    }
}

function mostrarModal(nomDiv) {
    var div = document.getElementById(nomDiv);
    div.style.visibility = 'visible';
}

function cerrarDiv(div) {
    div.parentNode.parentNode.parentNode.style.visibility = 'hidden';
    if (nModal == 2) foco("txtNroDeudor");
    if (nModal == 1) foco("txtNroMaterial" + idFila);
}

function foco(nomControl) {
    document.getElementById(nomControl).focus();
}

function buscarArticulo(e){
    if(e.key=="Enter")
    {
        alert("ENVIAR SOLICITU AL SERVIDOR");
    }
    if(e.key=="Escape")
    {
        document.getElementById("divBusqMateriales").style.visibility='hidden';
        limpiarBA();
        foco("txtNroMaterial" + idFila);
    }
}

function limpiarBA(){
    document.getElementById("GV_ZZ_DESCRIPCION").value = "";
    document.getElementById("GV_GROES").value = "";
    document.getElementById("GV_ZZ_APLICACION").value = "";
    document.getElementById("GV_MATNR").value = "";
}

function buscarCliente(e, nro) {
    if (e.key == "Enter") {
        alert("ENVIAR SOLICITUD AL SERVIDOR");
        if (nro == 1) {
        }
    }
    if (e.key == "Tab") {
        foco(nomControl);
    }
}

function buscarCliente(e,nro,nomControl) {
    if (e.key == "Enter") {
        alert("ENVIAR SOLICITUD AL SERVIDOR");
        if (nro == 1) {
        }
    }
    if (e.key == "Tab") {
        foco(nomControl);
    }
}


function validaNumero(e) {
    tecla = (document.all) ? e.keyCode : e.which;

    //Tecla de retroceso para borrar, siempre la permite
    if (tecla == 8) {
        return true;
    }

    // Patron de entrada, en este caso solo acepta numeros
    patron = /[0-9]/;
    tecla_final = String.fromCharCode(tecla);
    return patron.test(tecla_final);
}