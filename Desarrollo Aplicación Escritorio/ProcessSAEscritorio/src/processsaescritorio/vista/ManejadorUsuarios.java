/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package processsaescritorio.vista;
import java.time.LocalDate;
import java.time.Month;
import java.time.ZoneId;
import java.util.ArrayList;
import java.util.Date;
import javax.swing.JOptionPane;
import javax.swing.table.DefaultTableModel;
import processsaescritorio.controlador.*;
import processsaescritorio.modelo.*;


/**
 *
 * @author yo
 */
public class ManejadorUsuarios extends javax.swing.JInternalFrame {

    /**
     * Creates new form ManejadorUsuarios
     */
    
    ArrayList<UsuarioDTO> listaUsuarios;
    private boolean usuarioIdentificado=false;
    private int idGeneral=0;
    
    public ManejadorUsuarios() {
        initComponents();
        actualizarListaUsuarios();
        listadoCompania();
        listadoRegion();
        listadoGenero();
        cbxProvince.setEnabled(false);
        cbxCommune.setEnabled(false);
       
    }
    
    public void resetearTabla()
    {
        listaUsuarios.removeAll(listaUsuarios);
        actualizarListaUsuarios();
    }
    public void listadoCompania(){
          for (CompaniaDTO compania:new Lista().listarCompanias()) {
              cbxCompany.addItem(compania.getId()+"-"+compania.getNombre());
          }
    }
    
    public void listadoRegion(){
        
        for (RegionDTO region:new Lista().listarRegiones()) {
              cbxRegion.addItem(region.getId()+"-"+region.getNombre());
        }                  
    }
    
    public void listadoGenero(){
        
        for (GeneroDTO genero:new Lista().listarGeneroPersona()) {
              cbxGender.addItem(genero.getNombre());
        }                  
    }
    
    ///unidad interna buscada por undiad asignada por compañia
    public void listadoUnidadAsignada(){
       
        String[] arrayCompany = cbxCompany.getSelectedItem().toString().split("-");   
        int id_company=Integer.parseInt(arrayCompany[0]);
        
        
        for (UnidadAsignadaDTO unidadAsignada:new Lista().listarUnidadAsignadaPorCompania(id_company)) {
          
            String nombre=new Consulta().buscarunidad(unidadAsignada.getId_internalUnit());
            cbxAssignedUnit.addItem(unidadAsignada.getId()+"-"+nombre);
        }
    }

    
    public void listadoProvincia(){
        
        String[] arrayRegion= cbxRegion.getSelectedItem().toString().split("-");
        int id_region=Integer.parseInt(arrayRegion[0]);
        
        for (ProvinciaDTO provincia:new Lista().listarProvincias(id_region))
        {
            cbxProvince.addItem(provincia.getId()+"-"+provincia.getNombre());       
        }
    }
    
    
    public void listadoComuna(){
        
        String[] arrayRegion= cbxProvince.getSelectedItem().toString().split("-");
        int id_province=Integer.parseInt(arrayRegion[0]);
        
        
        for (ComunaDTO comuna:new Lista().listarComunasPorProvincia(id_province))
        {
            cbxCommune.addItem(comuna.getId()+"-"+comuna.getNombre());
   
        }
    }
    
    public Date convertToDateViaInstant(LocalDate dateToConvert) {
    return java.util.Date.from(dateToConvert.atStartOfDay()
      .atZone(ZoneId.systemDefault())
      .toInstant());
    }
    
    
    public LocalDate convertToLocalDate(Date dateToConvert){
    return dateToConvert.toInstant().atZone(ZoneId.systemDefault()).toLocalDate();}
    
    public void actualizarDatosUsuario(){
       
        new UsuarioDAO().actualizarUsuario(idGeneral,txtName.getText());

    }
    
    public void limpiarFormulario()
    {
        usuarioIdentificado=false;
        idGeneral=0;
        tblUsuario.getSelectionModel().clearSelection();
        txtName.setText("");
        txtLastName.setText("");
        txtPhone.setText("");
        txtAddress.setText("");
        txtEmail.setText("");
        txtEmail.setEditable(true);
        btnBorrar.setEnabled(false);

        cbxRegion.setSelectedIndex(cbxRegion.getSelectedIndex());
        cbxGender.setSelectedIndex(cbxGender.getSelectedIndex());
        cbxCompany.setSelectedIndex(cbxCompany.getSelectedIndex());
        cbxAssignedUnit.setSelectedIndex(cbxAssignedUnit.getSelectedIndex());
       //arreglar estas dos
    }
    
    public void actualizarListaUsuarios(){
        listaUsuarios = new Lista().listarUsuarios();
        String[] columnas = {"ID", "Nombre","Apellido"};
        DefaultTableModel modeloTabla = new DefaultTableModel(columnas, 0){
        @Override
        public boolean isCellEditable(int filas,int columnas){
            return false;
        }
        };
        
        for(UsuarioDTO usuario : listaUsuarios){
            String id       = String.valueOf(usuario.getId());
            String name     = String.valueOf(usuario.getFirstname());
            String lastName = String.valueOf(usuario.getLastname());
   
            Object[] elemento = {id,name,lastName};
            modeloTabla.addRow(elemento);
        };
        tblUsuario.setModel(modeloTabla);
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
        tblUsuario = new javax.swing.JTable();
        btnGrabar = new javax.swing.JButton();
        txtName = new javax.swing.JTextField();
        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel3 = new javax.swing.JLabel();
        txtLastName = new javax.swing.JTextField();
        txtEmail = new javax.swing.JTextField();
        dateBorn = new com.toedter.calendar.JDateChooser();
        jLabel4 = new javax.swing.JLabel();
        cbxCompany = new javax.swing.JComboBox<>();
        jLabel5 = new javax.swing.JLabel();
        jLabel9 = new javax.swing.JLabel();
        cbxGender = new javax.swing.JComboBox<>();
        txtPhone = new javax.swing.JTextField();
        jLabel10 = new javax.swing.JLabel();
        jLabel11 = new javax.swing.JLabel();
        cbxAssignedUnit = new javax.swing.JComboBox<>();
        txtAddress = new javax.swing.JTextField();
        jLabel6 = new javax.swing.JLabel();
        cbxCommune = new javax.swing.JComboBox<>();
        jLabel12 = new javax.swing.JLabel();
        cbxRegion = new javax.swing.JComboBox<>();
        jLabel13 = new javax.swing.JLabel();
        cbxProvince = new javax.swing.JComboBox<>();
        jLabel14 = new javax.swing.JLabel();
        btnRefrescar = new javax.swing.JButton();
        lbl = new javax.swing.JLabel();
        btnBorrar = new javax.swing.JButton();

        setClosable(true);
        setMaximizable(true);
        setTitle("Manejo de Usuarios");

        tblUsuario.setModel(new javax.swing.table.DefaultTableModel(
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
        tblUsuario.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                tblUsuarioMouseClicked(evt);
            }
        });
        jScrollPane1.setViewportView(tblUsuario);

        btnGrabar.setText("Grabar");
        btnGrabar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnGrabarActionPerformed(evt);
            }
        });

        jLabel1.setText("Nombre");

        jLabel2.setText("Apellido");

        jLabel3.setText("Correo");

        dateBorn.setDateFormatString("MM,dd, yyyy");

        jLabel4.setText("Fecha de nacimiento");

        cbxCompany.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cbxCompanyActionPerformed(evt);
            }
        });

        jLabel5.setText("Compañia");

        jLabel9.setText("Genero");

        cbxGender.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cbxGenderActionPerformed(evt);
            }
        });

        txtPhone.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                txtPhoneActionPerformed(evt);
            }
        });

        jLabel10.setText("Telefono");

        jLabel11.setText("Unidad Asignada");

        cbxAssignedUnit.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cbxAssignedUnitActionPerformed(evt);
            }
        });

        jLabel6.setText("Dirección");

        cbxCommune.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cbxCommuneActionPerformed(evt);
            }
        });

        jLabel12.setText("Comuna");

        cbxRegion.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cbxRegionActionPerformed(evt);
            }
        });

        jLabel13.setText("Region");

        cbxProvince.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                cbxProvinceActionPerformed(evt);
            }
        });

        jLabel14.setText("Provincia");

        btnRefrescar.setText("Refrescar");
        btnRefrescar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnRefrescarActionPerformed(evt);
            }
        });

        btnBorrar.setText("Eliminar");
        btnBorrar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnBorrarActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(29, 29, 29)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jLabel13)
                                    .addComponent(cbxRegion, javax.swing.GroupLayout.PREFERRED_SIZE, 119, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jLabel14)
                                    .addComponent(cbxProvince, javax.swing.GroupLayout.PREFERRED_SIZE, 119, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGap(18, 18, 18)
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jLabel12)
                                    .addComponent(cbxCommune, javax.swing.GroupLayout.PREFERRED_SIZE, 119, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGap(134, 134, 134))
                            .addGroup(layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addGroup(layout.createSequentialGroup()
                                        .addComponent(btnGrabar)
                                        .addGap(29, 29, 29)
                                        .addComponent(btnRefrescar)
                                        .addGap(18, 18, 18)
                                        .addComponent(btnBorrar))
                                    .addGroup(layout.createSequentialGroup()
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(txtEmail, javax.swing.GroupLayout.PREFERRED_SIZE, 254, javax.swing.GroupLayout.PREFERRED_SIZE)
                                            .addComponent(jLabel3))
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, 98, javax.swing.GroupLayout.PREFERRED_SIZE)
                                            .addComponent(dateBorn, javax.swing.GroupLayout.PREFERRED_SIZE, 184, javax.swing.GroupLayout.PREFERRED_SIZE)))
                                    .addGroup(layout.createSequentialGroup()
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(jLabel5)
                                            .addComponent(cbxCompany, javax.swing.GroupLayout.PREFERRED_SIZE, 189, javax.swing.GroupLayout.PREFERRED_SIZE))
                                        .addGap(31, 31, 31)
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(jLabel11)
                                            .addComponent(cbxAssignedUnit, javax.swing.GroupLayout.PREFERRED_SIZE, 119, javax.swing.GroupLayout.PREFERRED_SIZE))))
                                .addGap(0, 0, Short.MAX_VALUE))
                            .addGroup(layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addGroup(layout.createSequentialGroup()
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(txtAddress, javax.swing.GroupLayout.PREFERRED_SIZE, 254, javax.swing.GroupLayout.PREFERRED_SIZE)
                                            .addComponent(jLabel6))
                                        .addGap(18, 18, 18)
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(jLabel10)
                                            .addComponent(txtPhone, javax.swing.GroupLayout.PREFERRED_SIZE, 118, javax.swing.GroupLayout.PREFERRED_SIZE)))
                                    .addGroup(layout.createSequentialGroup()
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addGroup(layout.createSequentialGroup()
                                                .addComponent(txtName, javax.swing.GroupLayout.PREFERRED_SIZE, 157, javax.swing.GroupLayout.PREFERRED_SIZE)
                                                .addGap(24, 24, 24)
                                                .addComponent(txtLastName, javax.swing.GroupLayout.PREFERRED_SIZE, 164, javax.swing.GroupLayout.PREFERRED_SIZE))
                                            .addGroup(layout.createSequentialGroup()
                                                .addComponent(jLabel1)
                                                .addGap(144, 144, 144)
                                                .addComponent(jLabel2)))
                                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                            .addComponent(jLabel9)
                                            .addComponent(cbxGender, javax.swing.GroupLayout.PREFERRED_SIZE, 119, javax.swing.GroupLayout.PREFERRED_SIZE))))
                                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))))
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                        .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(lbl)
                        .addGap(208, 208, 208)))
                .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 349, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(25, 25, 25))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, layout.createSequentialGroup()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(49, 49, 49)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel1)
                            .addComponent(jLabel2)
                            .addComponent(jLabel9))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(txtName, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(txtLastName, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(cbxGender, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(24, 24, 24)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel6)
                            .addComponent(jLabel10))
                        .addGap(3, 3, 3)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                            .addGroup(layout.createSequentialGroup()
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                                    .addComponent(txtAddress, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                                    .addComponent(txtPhone, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                                .addGap(18, 18, 18)
                                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
                                    .addGroup(layout.createSequentialGroup()
                                        .addComponent(jLabel12)
                                        .addGap(3, 3, 3)
                                        .addComponent(cbxCommune, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                                    .addGroup(layout.createSequentialGroup()
                                        .addComponent(jLabel13)
                                        .addGap(3, 3, 3)
                                        .addComponent(cbxRegion, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))))
                            .addGroup(layout.createSequentialGroup()
                                .addComponent(jLabel14)
                                .addGap(3, 3, 3)
                                .addComponent(cbxProvince, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel3)
                            .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, 26, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(txtEmail, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(dateBorn, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addGap(18, 18, 18)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel11)
                            .addComponent(jLabel5))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(cbxCompany, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(cbxAssignedUnit, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(lbl)
                        .addGap(13, 13, 13)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(btnGrabar)
                            .addComponent(btnRefrescar)
                            .addComponent(btnBorrar)))
                    .addGroup(layout.createSequentialGroup()
                        .addContainerGap(23, Short.MAX_VALUE)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 396, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addGap(91, 91, 91))
        );

        setBounds(500, 150, 934, 544);
    }// </editor-fold>//GEN-END:initComponents

    private void btnGrabarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnGrabarActionPerformed
      
        if (usuarioIdentificado) {
            actualizarDatosUsuario();
        }else{
        LocalDate birthdate=convertToLocalDate(dateBorn.getDate()); ;
      
        String[] arrayCompany = cbxCompany.getSelectedItem().toString().split("-");   
        int id_company=Integer.parseInt(arrayCompany[0]);
        
        String[] arrayCommune = cbxCommune.getSelectedItem().toString().split("-");   
        int id_commune=Integer.parseInt(arrayCommune[0]);
        
        String[] arrayInternalU= cbxAssignedUnit.getSelectedItem().toString().split("-");   
         int id_unitAssig=Integer.parseInt(arrayInternalU[0]);
        
        int id_gender=3;
        if (cbxGender.getSelectedIndex()==0) {
            id_gender=1;
        }else if(cbxGender.getSelectedIndex()==1)
        {
            id_gender=2;
        }else
        {
            id_gender=3;
        }
        String pass=txtName.getText().substring(0,2)+txtLastName.getText().substring(0,2)+birthdate.getYear();
        new UsuarioDAO(0,txtName.getText(),txtLastName.getText(),txtAddress.getText(),txtPhone.getText(),birthdate,txtEmail.getText(),pass,id_commune,id_unitAssig,id_company,id_gender).crearUsuario(); 
        }
          resetearTabla();        
    }//GEN-LAST:event_btnGrabarActionPerformed

    private void cbxCompanyActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cbxCompanyActionPerformed
        if (cbxCompany.getSelectedItem()!=null) {
            cbxAssignedUnit.removeAllItems();
            listadoUnidadAsignada();
        }
    }//GEN-LAST:event_cbxCompanyActionPerformed

    private void cbxGenderActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cbxGenderActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_cbxGenderActionPerformed

    private void txtPhoneActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_txtPhoneActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_txtPhoneActionPerformed

    private void cbxAssignedUnitActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cbxAssignedUnitActionPerformed
     
    }//GEN-LAST:event_cbxAssignedUnitActionPerformed

    private void cbxCommuneActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cbxCommuneActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_cbxCommuneActionPerformed

    private void cbxRegionActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cbxRegionActionPerformed
         cbxProvince.setEnabled(true); 
          cbxProvince.removeAllItems();
          listadoProvincia();
    }//GEN-LAST:event_cbxRegionActionPerformed

    private void cbxProvinceActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_cbxProvinceActionPerformed
        if (cbxProvince.getSelectedItem()!=null) {
          cbxCommune.setEnabled(true); 
          cbxCommune.removeAllItems();
          listadoComuna();
        }
    }//GEN-LAST:event_cbxProvinceActionPerformed

    private void btnRefrescarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnRefrescarActionPerformed
       
      limpiarFormulario();
        
    }//GEN-LAST:event_btnRefrescarActionPerformed

    private void tblUsuarioMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_tblUsuarioMouseClicked
        int seleccion= tblUsuario.rowAtPoint(evt.getPoint());
        int id=Integer.parseInt(String.valueOf(tblUsuario.getValueAt(seleccion,0)));
        
        usuarioIdentificado=true;
        idGeneral=id;
        cbxProvince.setEnabled(true);
        cbxCommune.setEnabled(true);
        txtEmail.setEditable(false);
        btnBorrar.setEnabled(true);

       
        UsuarioDTO usuario=new UsuarioDAO().obtenerUsuarioPorIdBD(id);
        txtName.setText(usuario.getFirstname());
        txtLastName.setText(usuario.getLastname());
        txtPhone.setText(usuario.getPhone());
        txtEmail.setText(usuario.getEmail());
        txtAddress.setText(usuario.getAddress());

        
        String[] hola= new Consulta().listarPorComuna(usuario.getIdCommune());
        
        cbxRegion.setSelectedItem(hola[1]+"-"+hola[0]);
        cbxProvince.setSelectedItem(hola[3]+"-"+hola[2]);
        cbxCommune.setSelectedItem(hola[5]+"-"+hola[4]);
        
        dateBorn.setDate(convertToDateViaInstant(usuario.getBirthdate()));
       
        new Lista().listarCompanias().stream().filter((compania) -> (compania.getId()==usuario.getIdCompany())).forEachOrdered((compania) -> {
                cbxCompany.setSelectedItem(compania.toString());
        });
        new Lista().listarGeneroPersona().stream().filter((genero) -> (genero.getId()==usuario.getIdGender())).forEachOrdered((genero) -> {
                cbxGender.setSelectedItem(genero.toString());
        });
     
    }//GEN-LAST:event_tblUsuarioMouseClicked

    private void btnBorrarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnBorrarActionPerformed
               new Eliminar().eliminarUsuario(idGeneral);
               resetearTabla();
               limpiarFormulario();
    }//GEN-LAST:event_btnBorrarActionPerformed

    

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btnBorrar;
    private javax.swing.JButton btnGrabar;
    private javax.swing.JButton btnRefrescar;
    private javax.swing.JComboBox<String> cbxAssignedUnit;
    private javax.swing.JComboBox<String> cbxCommune;
    private javax.swing.JComboBox<String> cbxCompany;
    private javax.swing.JComboBox<String> cbxGender;
    private javax.swing.JComboBox<String> cbxProvince;
    private javax.swing.JComboBox<String> cbxRegion;
    private com.toedter.calendar.JDateChooser dateBorn;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel10;
    private javax.swing.JLabel jLabel11;
    private javax.swing.JLabel jLabel12;
    private javax.swing.JLabel jLabel13;
    private javax.swing.JLabel jLabel14;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel9;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JLabel lbl;
    private javax.swing.JTable tblUsuario;
    private javax.swing.JTextField txtAddress;
    private javax.swing.JTextField txtEmail;
    private javax.swing.JTextField txtLastName;
    private javax.swing.JTextField txtName;
    private javax.swing.JTextField txtPhone;
    // End of variables declaration//GEN-END:variables
}
