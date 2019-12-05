/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package processsaescritorio.vista;

import javax.swing.JOptionPane;
import processsaescritorio.controlador.Consulta;
import processsaescritorio.modelo.Global;
import processsaescritorio.modelo.RolPermisosDAO;
import processsaescritorio.modelo.RolPermisosDTO;
import processsaescritorio.modelo.UsuarioDTO;

/**
 *
 * @author Brayan
 */
public class VentanaPrincipal extends javax.swing.JFrame {

    /**
     * Creates new form NewJFrame
     */
    private UsuarioDTO usuarioSesion;
    private Login ventanaPadre;
    
    VentanaPrincipal(UsuarioDTO usuarioSesion, Login aThis) {
        initComponents();
        this.setSize(1080, 900);
     //    this.setExtendedState(MAXIMIZED_BOTH);
       this.setLocationRelativeTo(null);
        this.usuarioSesion = usuarioSesion;
        this.ventanaPadre = ventanaPadre;
        RolPermisosDTO permisosU= new RolPermisosDAO().validarPermisoUsuarioBD(usuarioSesion.getId(),31);
    //JOptionPane.showMessageDialog(null, "Probando"+usuarioSesion.getId(), "Grabar", JOptionPane.PLAIN_MESSAGE);
    //JOptionPane.showMessageDialog(null, "Probando"+  permisosU.getIdRol(), "Grabar", JOptionPane.PLAIN_MESSAGE);

        if (permisosU.getIdRol()==2) {
            menuAdminstracion.setEnabled(false);
            menuAdminstracion.setVisible(false);
          
        }
    }
    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        panelEscritorio = new javax.swing.JDesktopPane();
        jMenuBar1 = new javax.swing.JMenuBar();
        menuAdminstracion = new javax.swing.JMenu();
        jMenuItem1 = new javax.swing.JMenuItem();
        jMenuItem5 = new javax.swing.JMenuItem();
        jMenuItem2 = new javax.swing.JMenuItem();
        menuUnidadInterna = new javax.swing.JMenuItem();
        jMenu1 = new javax.swing.JMenu();
        menuFlujoTarea = new javax.swing.JMenuItem();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setBackground(new java.awt.Color(51, 204, 255));

        panelEscritorio.setBackground(new java.awt.Color(255, 255, 255));
        panelEscritorio.setCursor(new java.awt.Cursor(java.awt.Cursor.DEFAULT_CURSOR));

        javax.swing.GroupLayout panelEscritorioLayout = new javax.swing.GroupLayout(panelEscritorio);
        panelEscritorio.setLayout(panelEscritorioLayout);
        panelEscritorioLayout.setHorizontalGroup(
            panelEscritorioLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 836, Short.MAX_VALUE)
        );
        panelEscritorioLayout.setVerticalGroup(
            panelEscritorioLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 428, Short.MAX_VALUE)
        );

        menuAdminstracion.setText("Administración");

        jMenuItem1.setText("Usuarios");
        jMenuItem1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem1ActionPerformed(evt);
            }
        });
        menuAdminstracion.add(jMenuItem1);

        jMenuItem5.setText("Compañia");
        jMenuItem5.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem5ActionPerformed(evt);
            }
        });
        menuAdminstracion.add(jMenuItem5);

        jMenuItem2.setText("Roles");
        jMenuItem2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jMenuItem2ActionPerformed(evt);
            }
        });
        menuAdminstracion.add(jMenuItem2);

        menuUnidadInterna.setText("Unidades Internas");
        menuUnidadInterna.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuUnidadInternaActionPerformed(evt);
            }
        });
        menuAdminstracion.add(menuUnidadInterna);

        jMenuBar1.add(menuAdminstracion);

        jMenu1.setText("Procesos");

        menuFlujoTarea.setText("Flujo de tareas");
        menuFlujoTarea.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                menuFlujoTareaActionPerformed(evt);
            }
        });
        jMenu1.add(menuFlujoTarea);

        jMenuBar1.add(jMenu1);

        setJMenuBar(jMenuBar1);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(panelEscritorio)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(panelEscritorio)
        );

        setBounds(0, 0, 852, 488);
    }// </editor-fold>//GEN-END:initComponents

    private void jMenuItem1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem1ActionPerformed
        ManejadorUsuarios mantenedorUsuario = new ManejadorUsuarios();
        panelEscritorio.add(mantenedorUsuario);
        mantenedorUsuario.setVisible(true); 
    }//GEN-LAST:event_jMenuItem1ActionPerformed

    private void jMenuItem5ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem5ActionPerformed
        ManejadorCompania mantenedorCompania = new ManejadorCompania();
        panelEscritorio.add(mantenedorCompania);
        mantenedorCompania.setVisible(true); 
    }//GEN-LAST:event_jMenuItem5ActionPerformed

    private void jMenuItem2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jMenuItem2ActionPerformed
        ManejadorRol mantenedorRol = new ManejadorRol();
        panelEscritorio.add(mantenedorRol);
        mantenedorRol.setVisible(true);       
    }//GEN-LAST:event_jMenuItem2ActionPerformed

    private void menuFlujoTareaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuFlujoTareaActionPerformed
        FlujoTareas flujo = new FlujoTareas();
        panelEscritorio.add(flujo);
        flujo.setVisible(true); 
    }//GEN-LAST:event_menuFlujoTareaActionPerformed

    private void menuUnidadInternaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_menuUnidadInternaActionPerformed
        ManejadorUnidadInterna unidad = new ManejadorUnidadInterna();
        panelEscritorio.add(unidad);
        unidad.setVisible(true); 
    }//GEN-LAST:event_menuUnidadInternaActionPerformed


    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JMenu jMenu1;
    private javax.swing.JMenuBar jMenuBar1;
    private javax.swing.JMenuItem jMenuItem1;
    private javax.swing.JMenuItem jMenuItem2;
    private javax.swing.JMenuItem jMenuItem5;
    private javax.swing.JMenu menuAdminstracion;
    private javax.swing.JMenuItem menuFlujoTarea;
    private javax.swing.JMenuItem menuUnidadInterna;
    private javax.swing.JDesktopPane panelEscritorio;
    // End of variables declaration//GEN-END:variables
}
