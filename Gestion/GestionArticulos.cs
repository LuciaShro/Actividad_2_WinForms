﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Gestion
{
    public class GestionArticulos
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
             
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion AS Marca, A.IdCategoria, C.Descripcion AS Categoria, A.Precio, I.ImagenUrl FROM ARTICULOS A LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id LEFT JOIN IMAGENES I ON A.Id = I.IdArticulo;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo(); //Lo empezamos a cargar con los datos del lector de ese registro
                    //aux.IDArticulo = lector.GetInt32(0); 
                    aux.IDArticulo = (int)datos.Lector["Id"]; //Lo mismo que la linea de arriba
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.codArticulo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    /*if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Nombre = (string)datos.Lector["Marca"]; */
                    aux.Marca.Nombre = datos.Lector["Marca"] is DBNull ? "" : (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                   /* if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Nombre = (string)datos.Lector["Categoria"];*/
                    aux.Categoria.Nombre = datos.Lector["Categoria"] is DBNull ? "" : (string)datos.Lector["Categoria"];


                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Imagen = new Imagen();
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.Imagen.ImagenURL = (string)datos.Lector["ImagenUrl"]; 

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            } 
            finally
            {
                datos.cerrarConexion();
            }
         
        }

        public void AgregarArticulos(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)values('" + articulo.codArticulo + "', '" + articulo.Nombre + "', '" + articulo.Descripcion + "', " + articulo.Marca.Id + ", " + articulo.Categoria.Id + ", " + articulo.Precio + ")");
                datos.ejecutarAccion();
                //datos.setearConsulta("UPDATE IMAGENES set ImagenUrl = @ImagenUrl WHERE Id = @Id");
                //datos.setearParametro("@ImagenUrl", articulo.Imagen.ImagenURL);
                //datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarArticulo(Articulo articulo) {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE ARTICULOS set Codigo = @Codigo, Nombre= @Nombre, Descripcion = @Descripcion, Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria where Id = @Id");
                datos.setearParametro("@Codigo", articulo.codArticulo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@IdMarca", articulo.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametro("@Id", articulo.IDArticulo);
                datos.ejecutarAccion();

                //datos.setearConsulta("UPDATE IMAGENES set ImagenUrl = @ImagenUrl WHERE Id = @Id");
                //datos.setearParametro("@ImagenUrl", articulo.Imagen.ImagenURL);
                //datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}

//Modificar articulos
//public bool ModificarArticulo(int codArticulo)
//{
//}

//Eliminar articulos
//public bool EliminarArticulo(int codArticulo) 
//{ 
//}

//Buscar articulos
//public Articulo BuscarArticulo(int codArticulo)
//{
//}

//Listar articulo
//public void ListarArticulos()
//{
//}