using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication3.Models.Clases;

namespace WebApplication3.Sources.Validations
{
    public  class mValidaciones 
    {
        static Regex rx = new Regex(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");

        public static ModelStateDictionary Validar_Usuario(Usuario usuario) {
            ModelStateDictionary ModelState = new ModelStateDictionary();
            if (string.IsNullOrEmpty(usuario.nombre)) {
                ModelState.AddModelError("nombre","Ingrese nombre");
            }
            if (string.IsNullOrEmpty(usuario.direccion)) {
                ModelState.AddModelError("direccion", "Ingrese una Direccion valida");
            }
                   
            if (string.IsNullOrEmpty(usuario.email)||!rx.IsMatch(usuario.email)) {
                ModelState.AddModelError("email", "Ingrese un Email valida");
            }
            if (string.IsNullOrEmpty(usuario.num_documento)) {
                ModelState.AddModelError("num_documento", "Ingrese un numero de Documento Valido ");
            }
            if (string.IsNullOrEmpty(usuario.tipo_documento)) {
                ModelState.AddModelError("tipo_documento", "Ingrese un tipo de Documento Valido ");
            }
            if (string.IsNullOrEmpty(usuario.telefono) ||usuario.telefono.Length > 10 )
            {
                ModelState.AddModelError("telefono", "Ingrese un numero Valido ");
            }
            if ( usuario.idrol ==null || usuario.idrol < 1)
            {
                ModelState.AddModelError("idrol", "Ingrese un Rol Valido ");
            }
         /*   if (string.IsNullOrEmpty(usuario.password_hash)  ) {
                ModelState.AddModelError("password_hash", "Ingrese una contraseña");
            }
            if (string.IsNullOrEmpty(usuario.password_salt))
            {
                ModelState.AddModelError("password_salt", "Ingrese un Rol Valido ");
            }*/
            return ModelState;
        }
        public static void  Validar_Rol(Rol rol,
            ModelStateDictionary modelState) {
            //modelState = new ModelStateDictionary();
            if (string.IsNullOrEmpty(rol.nombre)) modelState.AddModelError("nombre", "Ingrese un nombre valido");
            if (string.IsNullOrEmpty(rol.descripcion)) modelState.AddModelError("descripcion", "Ingrese una descripcion");
            
        }
        public static ModelStateDictionary Validar_Articulo(Articulo articulo)
        {
            ModelStateDictionary ModelState = new ModelStateDictionary();
            if (string.IsNullOrEmpty(articulo.nombre))
            {
                ModelState.AddModelError("nombre", "Ingrese nombre");
            }
            if (string.IsNullOrEmpty(articulo.codigo))
            {
                ModelState.AddModelError("codigo", "Ingrese un código válido ");
            }
            if (string.IsNullOrEmpty(articulo.descripcion))
            {
                ModelState.AddModelError("descripcion", "Ingrese una descripción");
            }
            if (articulo.precio_venta == null || articulo.precio_venta < 0)
            {
                ModelState.AddModelError("precioventa", "Ingrese una precio valido");
            }
            if (articulo.stock == null || articulo.stock<0)
            {
                ModelState.AddModelError("codigo", "Ingrese un stock valido");
            }
            if (articulo.idcategoria == null || articulo.idcategoria<0)
            {
                ModelState.AddModelError("idcategoria", "Ingrese una categoria valida ");
            }
            return ModelState;
        }
        public  ModelStateDictionary Validar_Categoria(Categoria categoria) {
            
            ModelStateDictionary ModelState = new ModelStateDictionary();
            if (String.IsNullOrEmpty(categoria.nombre)) {
                ModelState.AddModelError("nombre", "Ingrese un nombre");
            }
            if (String.IsNullOrEmpty(categoria.descripcion)) {
                ModelState.AddModelError("descripcion", "Ingrese una descripcion");
            }
            return ModelState;

        }
    }

}