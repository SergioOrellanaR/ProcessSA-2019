/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package processsaescritorio.modelo;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
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
public class ComunaDAO implements DatosConexion {

    private int id;

   
    private String nombre;
    private int Provincia;

    public ComunaDAO() {
    }

    public ComunaDAO(int id,String nombre, int Provincia) {
        this.id=id;
        this.nombre = nombre;
        this.Provincia = Provincia;
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

    public int getProvincia() {
        return Provincia;
    }

    public void setProvincia(int Provincia) {
        this.Provincia = Provincia;
    }
    
    public ArrayList<ComunaDTO> listarComunasPorProvincia(int id){
        ArrayList<ComunaDTO> listarComuna = new ArrayList<ComunaDTO>();
        try{
            Class.forName(DRIVER);
            Connection conexion =  DriverManager.getConnection(URL,USUARIO,CLAVE);
            Statement declaracion = conexion.createStatement();
            ResultSet resultado = declaracion.executeQuery("SELECT commune.name,province.id,commune.id from commune JOIN province ON (province.ID = commune.id_province ) WHERE province.id=" + id);
            while (resultado.next()) {
                this.setNombre(resultado.getString(1));     
                this.setProvincia(resultado.getInt(2));
                this.setId(resultado.getInt(3));

                
  
                listarComuna.add(new ComunaDTO(this.getId(),this.getNombre(),this.getProvincia()));
            }  
            conexion.close();
            return listarComuna;
        }catch(Exception e){
            System.out.println("Error : " + e);
            return listarComuna;
        }
    }
    
    public  String[] listarProporComuna(int id){
    ArrayList<String> a = new ArrayList<String>();
   try{
    Class.forName(DRIVER);
    Connection conexion = DriverManager.getConnection(URL,USUARIO,CLAVE);
     Statement declaracion = conexion.createStatement();
            ResultSet resultado = declaracion.executeQuery("SELECT Region.name,Region.id,Province.name,Province.id,commune.name,commune.id FROM Region INNER JOIN (Province INNER JOIN commune ON commune.id_province =Province.id) ON Province.id_region =Region.id where commune.id =" + id);
    while(resultado.next())
    {
        a.add(resultado.getString(1));
        a.add(resultado.getString(2));
        a.add(resultado.getString(3));
        a.add(resultado.getString(4));
        a.add(resultado.getString(5));
        a.add(resultado.getString(6));

    }

    return (String[]) a.toArray(new String[a.size()]);
    }catch(Exception e){
            System.out.println("Error : " + e);
            return null;
        }
    }
    }
