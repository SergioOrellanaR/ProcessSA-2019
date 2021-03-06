/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package processsaescritorio.modelo;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import static processsaescritorio.modelo.DatosConexion.CLAVE;
import static processsaescritorio.modelo.DatosConexion.DRIVER;
import static processsaescritorio.modelo.DatosConexion.URL;
import static processsaescritorio.modelo.DatosConexion.USUARIO;

/**
 *
 * @author yo
 */
public class TareaDAO implements DatosConexion {
    
    private int id;
    private String nombre,descripcion;
    private int predefinido;
    private int activo;
    private int idTareaSuperior;
    private int idDependenciaTarea;

    public TareaDAO() {
    }

    public TareaDAO(int id, String nombre, String descripcion, int predefinido, int activo, int idTareaSuperior, int idDependenciaTarea) {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.predefinido = predefinido;
        this.activo = activo;
        this.idTareaSuperior = idTareaSuperior;
        this.idDependenciaTarea = idDependenciaTarea;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public int getPredefinido() {
        return predefinido;
    }

    public void setPredefinido(int predefinido) {
        this.predefinido = predefinido;
    }

    public int getActivo() {
        return activo;
    }

    public void setActivo(int activo) {
        this.activo = activo;
    }

    public int getIdTareaSuperior() {
        return idTareaSuperior;
    }

    public void setIdTareaSuperior(int idTareaSuperior) {
        this.idTareaSuperior = idTareaSuperior;
    }

    public int getIdDependenciaTarea() {
        return idDependenciaTarea;
    }

    public void setIdDependenciaTarea(int idDependenciaTarea) {
        this.idDependenciaTarea = idDependenciaTarea;
    }

        public ArrayList<TareaDTO> ObtenerTareas(){
        
        ArrayList<TareaDTO> listaTareas= new ArrayList<TareaDTO>();
        try{
            Class.forName(DRIVER);
            Connection conexion =  DriverManager.getConnection(URL,USUARIO,CLAVE);
            Statement declaracion = conexion.createStatement();
            ResultSet resultado = declaracion.executeQuery("select id,name,description,ispredefined,isactive,id_superior_task,id_dependent_task from task");
            while (resultado.next()) {
                this.setId(resultado.getInt(1));
                this.setNombre(resultado.getString(2));
                this.setDescripcion(resultado.getString(3));
                this.setPredefinido(resultado.getInt(4));
                this.setActivo(resultado.getInt(5));
                this.setIdTareaSuperior(resultado.getInt(6));
                this.setIdDependenciaTarea(resultado.getInt(7));
                listaTareas.add(new TareaDTO(this.getId(),this.getNombre(),this.getDescripcion(),this.getPredefinido(),this.getActivo(),this.getIdTareaSuperior(),this.getIdTareaSuperior()));
                    //  listaRegiones.add(new RegionDTO(this.getId(),this.getNombre()));              
            }  
            conexion.close();
            return listaTareas;
        }catch(Exception e){
            System.out.println("Error : " + e);
            return listaTareas;
        }
    } 
    
    
}
