/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package processsaescritorio.vista;

import java.util.ArrayList;
import javax.swing.table.DefaultTableModel;
import processsaescritorio.controlador.Lista;
import processsaescritorio.modelo.*;

/**
 *
 * @author yo
 */
public class FlujoTareas extends javax.swing.JInternalFrame {

    /**
     * Creates new form FlujoTareas
     */
    
    ArrayList<TareaDTO> listaTareas;
    private int idGeneral=0;
    
    public FlujoTareas() {
        initComponents();
        actualizarListaTarea();
    }
    
    
        public void actualizarListaTarea(){
        listaTareas = new Lista().listarTareas();
        String[] columnas = {"ID", "Nombre","Descripcion","Activo"};
        DefaultTableModel modeloTabla = new DefaultTableModel(columnas, 0){
        @Override
        public boolean isCellEditable(int filas,int columnas){
            return false;
        }
        };
        
        for(TareaDTO tarea : listaTareas){
            String id       = String.valueOf(tarea.getId());
            String name     = String.valueOf(tarea.getNombre());
            String descripcion     = String.valueOf(tarea.getDescripcion());
            String activo=Integer.valueOf(tarea.getActivo())==1?"Activo":"Inactivo";
         
          
            Object[] elemento = {id,name,descripcion,activo};
            modeloTabla.addRow(elemento);
        };
        tblTareas.setModel(modeloTabla);
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jScrollPane1 = new javax.swing.JScrollPane();
        tblTareas = new javax.swing.JTable();

        tblTareas.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}
            },
            new String [] {
                "Title 1", "Title 2", "Title 3", "Title 4"
            }
        ));
        tblTareas.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                tblTareasMouseClicked(evt);
            }
        });
        jScrollPane1.setViewportView(tblTareas);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addContainerGap(454, Short.MAX_VALUE)
                .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 430, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 292, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(182, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void tblTareasMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_tblTareasMouseClicked
             int seleccion= tblTareas.rowAtPoint(evt.getPoint());
        int id=Integer.parseInt(String.valueOf(tblTareas.getValueAt(seleccion,0)));
        
        //companiaIdentificada=true;
        idGeneral=id;
       // btnBorrar.setEnabled(true);
    }//GEN-LAST:event_tblTareasMouseClicked


    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JTable tblTareas;
    // End of variables declaration//GEN-END:variables
}
