package main;

import controllers.GenericController;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;
import javax.swing.DefaultListModel;
import models.AttributeSheet;
import models.Element;
import models.Karaimon;

public class MonCreator extends javax.swing.JFrame {

    List<Karaimon> mons;
    List<Element> elements;
    Karaimon selectedMon;
    
    public MonCreator() {
        initComponents();
        
        loadElements();
        loadMons();
    }

    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        nameEdit = new javax.swing.JTextField();
        strenghtEdit = new javax.swing.JTextField();
        jLabel3 = new javax.swing.JLabel();
        agilityEdit = new javax.swing.JTextField();
        jLabel4 = new javax.swing.JLabel();
        toughnessEdit = new javax.swing.JTextField();
        saveButton = new javax.swing.JButton();
        deleteButton = new javax.swing.JButton();
        jLabel5 = new javax.swing.JLabel();
        codeEdit = new javax.swing.JTextField();
        jScrollPane1 = new javax.swing.JScrollPane();
        elementList = new javax.swing.JList();
        jScrollPane2 = new javax.swing.JScrollPane();
        monList = new javax.swing.JList();
        newButton = new javax.swing.JButton();
        jButton1 = new javax.swing.JButton();
        jButton2 = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setResizable(false);

        jLabel1.setText("Name:");

        jLabel2.setText("Strenght:");

        jLabel3.setText("Agility:");

        jLabel4.setText("Toughness:");

        saveButton.setText("Save");
        saveButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                saveButtonActionPerformed(evt);
            }
        });

        deleteButton.setText("Delete");
        deleteButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                deleteButtonActionPerformed(evt);
            }
        });

        jLabel5.setText("Code:");

        codeEdit.setEnabled(false);

        elementList.setSelectionMode(javax.swing.ListSelectionModel.SINGLE_SELECTION);
        jScrollPane1.setViewportView(elementList);

        monList.setSelectionMode(javax.swing.ListSelectionModel.SINGLE_SELECTION);
        monList.addListSelectionListener(new javax.swing.event.ListSelectionListener() {
            public void valueChanged(javax.swing.event.ListSelectionEvent evt) {
                monListValueChanged(evt);
            }
        });
        jScrollPane2.setViewportView(monList);

        newButton.setText("New");
        newButton.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                newButtonActionPerformed(evt);
            }
        });

        jButton1.setText("All Attacks");
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        jButton2.setText("Attacks");
        jButton2.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton2ActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jScrollPane2, javax.swing.GroupLayout.DEFAULT_SIZE, 131, Short.MAX_VALUE)
                    .addComponent(jButton1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(newButton, javax.swing.GroupLayout.PREFERRED_SIZE, 53, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jButton2, javax.swing.GroupLayout.PREFERRED_SIZE, 73, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(saveButton)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(deleteButton))
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                            .addComponent(jLabel4)
                            .addComponent(jLabel1)
                            .addComponent(jLabel5)
                            .addComponent(jLabel3)
                            .addComponent(jLabel2))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                            .addComponent(codeEdit, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(nameEdit, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(strenghtEdit, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(agilityEdit, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(toughnessEdit, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 87, javax.swing.GroupLayout.PREFERRED_SIZE)))
                    .addComponent(jScrollPane1))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel5)
                            .addComponent(codeEdit, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel1)
                            .addComponent(nameEdit, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel2)
                            .addComponent(strenghtEdit, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel3)
                            .addComponent(agilityEdit, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                            .addComponent(jLabel4)
                            .addComponent(toughnessEdit, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 21, Short.MAX_VALUE)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 86, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(jScrollPane2))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(saveButton)
                    .addComponent(deleteButton)
                    .addComponent(newButton)
                    .addComponent(jButton1)
                    .addComponent(jButton2))
                .addContainerGap())
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void monListValueChanged(javax.swing.event.ListSelectionEvent evt) {//GEN-FIRST:event_monListValueChanged
        int i = monList.getSelectedIndex();
        if (i == -1)
            return;
        
        Karaimon e = mons.get(i);
        selectedMon = e;
        reloadForm();
    }//GEN-LAST:event_monListValueChanged

    private void newButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_newButtonActionPerformed
        selectedMon = null;
        codeEdit.setText(null);
        nameEdit.setText(null);
        strenghtEdit.setText(null);
        agilityEdit.setText(null);
        toughnessEdit.setText(null);
        elementList.clearSelection();
        monList.clearSelection();
    }//GEN-LAST:event_newButtonActionPerformed

    private void saveButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_saveButtonActionPerformed
        if (selectedMon != null) {
            AttributeSheet as = selectedMon.getAttributes();
            as.setStrenght(Float.parseFloat(strenghtEdit.getText()));
            as.setAgility(Float.parseFloat(agilityEdit.getText()));
            as.setToughness(Float.parseFloat(toughnessEdit.getText()));
            new GenericController<>(AttributeSheet.class).update(as, as.getId());
            
            selectedMon.setElement(elements.get(elementList.getSelectedIndex()));
            selectedMon.setName(nameEdit.getText());
            new GenericController<>(Karaimon.class).update(selectedMon, selectedMon.getId());
        } else {
            AttributeSheet as = new AttributeSheet();
            as.setStrenght(Float.parseFloat(strenghtEdit.getText()));
            as.setAgility(Float.parseFloat(agilityEdit.getText()));
            as.setToughness(Float.parseFloat(toughnessEdit.getText()));
            as = new GenericController<>(AttributeSheet.class).insert(as);
            
            Karaimon mon = new Karaimon();
            mon.setAttributes(as);
            mon.setElement(elements.get(elementList.getSelectedIndex()));
            mon.setName(nameEdit.getText());
            mon = new GenericController<>(Karaimon.class).insert(mon);
            if (mon.getId() != null && mon.getId() != 0) {
                System.out.println("Successfully added.");
            }
        }
        
        loadElements();
        loadMons();
    }//GEN-LAST:event_saveButtonActionPerformed

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        AttackCreator attackCreator = new AttackCreator();
        attackCreator.setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        attackCreator.setVisible(true);
    }//GEN-LAST:event_jButton1ActionPerformed

    private void deleteButtonActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_deleteButtonActionPerformed
        new GenericController<>(Karaimon.class).delete(selectedMon.getId());
        loadMons();
        selectedMon = null;
        
        monList.setSelectedIndex(-1);
        
        reloadForm();
    }//GEN-LAST:event_deleteButtonActionPerformed

    private void jButton2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton2ActionPerformed
        if (selectedMon == null)
            return;
        
        AttackManager attackManager = new AttackManager();
        attackManager.setMon(selectedMon);
        attackManager.setDefaultCloseOperation(DISPOSE_ON_CLOSE);
        attackManager.setVisible(true);
    }//GEN-LAST:event_jButton2ActionPerformed

    void reloadForm() {
        if (selectedMon == null) {
            codeEdit.setText(null);
            nameEdit.setText(null);
            strenghtEdit.setText(null);
            agilityEdit.setText(null);
            toughnessEdit.setText(null);
            elementList.setSelectedIndex(-1);
            
            return;
        }
        codeEdit.setText(selectedMon.getId().toString());
        nameEdit.setText(selectedMon.getName());
        strenghtEdit.setText(selectedMon.getAttributes().getStrenght().toString());
        agilityEdit.setText(selectedMon.getAttributes().getAgility().toString());
        toughnessEdit.setText(selectedMon.getAttributes().getToughness().toString());

        Integer elementId = selectedMon.getElement().getId();
        for (int i = 0; i < elements.size(); i++) {
            Element s = elements.get(i);
            if (s.getId().compareTo(elementId) == 0) {
                elementList.setSelectedIndex(i);
                break;
            }
        }
    }
    
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html 
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(MonCreator.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(MonCreator.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(MonCreator.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(MonCreator.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new MonCreator().setVisible(true);
            }
        });
    }

    void loadElements() {
        elements = new GenericController<>(Element.class).get();
        Collections.sort(elements);
        
        DefaultListModel listModel = new DefaultListModel();
        for (Element e : elements)
            listModel.addElement(e.getName());
        elementList.setModel(listModel);
    }
    
    void loadMons() {
        mons = new GenericController<>(Karaimon.class).get();
        DefaultListModel listModel = new DefaultListModel();
        for (Karaimon mon : mons)
            listModel.addElement(mon.getName());
        monList.setModel(listModel);
    }
    
    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JTextField agilityEdit;
    private javax.swing.JTextField codeEdit;
    private javax.swing.JButton deleteButton;
    private javax.swing.JList elementList;
    private javax.swing.JButton jButton1;
    private javax.swing.JButton jButton2;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JList monList;
    private javax.swing.JTextField nameEdit;
    private javax.swing.JButton newButton;
    private javax.swing.JButton saveButton;
    private javax.swing.JTextField strenghtEdit;
    private javax.swing.JTextField toughnessEdit;
    // End of variables declaration//GEN-END:variables
}
