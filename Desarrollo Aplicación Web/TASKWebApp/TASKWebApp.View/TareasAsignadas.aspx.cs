﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TASKWebApp.Business.Classes;
using System.Drawing;

namespace TASKWebApp.View
{
    public partial class TareasAsignadas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["ses"];
            try
            {
                int assignedStatus = 1;
                int reassignedStatus = 4;
                List<ProcessedTask> activeAndReasignedTasks = user.SearchProcessedTaskByStatus(assignedStatus, reassignedStatus, true);
                LoadTaskInformation(activeAndReasignedTasks);
            }
            catch
            {
                Response.Redirect("Home.aspx");
            }
            
        }

        private void LoadTaskInformation(List<ProcessedTask> tasks)
        {
            if(tasks.Count > 0)
            {
                repTabla.DataSource = tasks;
                repTabla.DataBind();

                for (int i = 0; i < tasks.Count; i++)
                {
                    RepeaterItem item = repTabla.Items[i];
                    SetRowInTableInformation(item, tasks[i]);
                }
            }
            else
            {
                Label7.Text = "Usted no tiene tareas pendientes de revisión!";
            }
            
        }

        private void SetRowInTableInformation(RepeaterItem item, ProcessedTask processedTask)
        {
            Color color = GetKPIColor(processedTask);

            SetTableindividualLabelInformation("lblSubNombre", processedTask.TaskAssignment.Task.Name, item, color);
            SetTableindividualLabelInformation("lblAsignadaPor", processedTask.TaskAssignment.AssignerUser.FullName(), item, color);
            SetTableindividualLabelInformation("lblSubDescripcion", processedTask.TaskAssignment.Task.Description, item, color);
            SetTableindividualLabelInformation("lblSubFechaInicio", processedTask.StartDate.ToString(), item, color);
            SetTableindividualLabelInformation("lblSubFechaFin", processedTask.EndDate.ToString(), item, color);

            if (processedTask.TaskAssignment.Task.DependentTask != null)
            {
                SetTableindividualLabelInformation("lblSubDependencia", processedTask.TaskAssignment.Task.DependentTask.Name, item, color);
            }
            else
            {
                SetTableindividualLabelInformation("lblSubDependencia", "Ninguna", item, color);
            }

            SetTableindividualLabelInformation("lblIdTarea", processedTask.Id.ToString(), item, color);
        }

        private Color GetKPIColor(ProcessedTask processedTask)
        {
            Color color;
            DateTime now = DateTime.Now;

            if (processedTask.StartDate < now)
                color = Color.Red;
            else if (processedTask.StartDate.Date == now.Date)
                color = Color.DarkKhaki;
            else
                color = Color.Green;

            return color;
        }

        private void SetTableindividualLabelInformation(string labelName, string information, RepeaterItem item, Color color)
        {
            Label label = (Label)item.FindControl(labelName);
            label.Text = information;
            label.ForeColor = color;
        }

        protected void btnSubAceptar_Click(object sender, EventArgs e)
        {
            int InProcessId = 2;
            int errorCode = -1;
            LinkButton button = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            int rowTaskId = GetRowTaskId(item); 

            if (rowTaskId != errorCode)
            {
                ProcessedTask processedTask = new ProcessedTask() { Id = rowTaskId };
                processedTask.Read();
                processedTask.IdTaskStatus = InProcessId;
                processedTask.Update();
            }
            
            Response.Redirect("TareasAsignadas.aspx");
        }

        protected void btnSubRechazar_Click(object sender, EventArgs e)
        {

            LinkButton button = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            ProcessedTask processedTask = new ProcessedTask() { Id = GetRowTaskId(item) };
            processedTask.Read();
            LoadRejectForm(processedTask);
        }

        private int GetRowTaskId(RepeaterItem item)
        {
            try
            {
                int index = item.ItemIndex;
                RepeaterItem IndItem = repTabla.Items[index];
                Label label = (Label)IndItem.FindControl("lblIdTarea");
                int idAssigned = int.Parse(label.Text);
                return idAssigned;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        private void LoadRejectForm(ProcessedTask processedTask)
        {
            lblMessage.Text = string.Empty;
            divTareaRechazada.Visible = true;
            lblNombreTareaRechazada.Text = processedTask.TaskAssignment.Task.Name;
            lblInternalId.Text = processedTask.Id.ToString();
            
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            int rejectedStatusId = 5;

            if(string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                lblMessage.Text = "Motivo de rechazo no puede ser vacío";
            }
            else
            {
                ProcessedTask processedTask = new ProcessedTask() { Id = int.Parse(lblInternalId.Text) };
                if(processedTask.Read() && processedTask.TaskAssignment.ReceiverUser.Id == ((User)Session["ses"]).Id)
                {
                    processedTask.IdTaskStatus = rejectedStatusId;
                    processedTask.Commentary = txtDescription.Text;
                    processedTask.Update();
                    Response.Redirect("TareasAsignadas.aspx");
                }
                else
                {
                    lblMessage.Text = "Error desconocido.";
                }
            }
        }
    }
}