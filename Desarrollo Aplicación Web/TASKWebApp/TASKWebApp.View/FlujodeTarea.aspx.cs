﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TASKWebApp.Business.Classes;
using TASKWebApp.Business.Helpers;

namespace TASKWebApp.View
{
    public partial class FlujodeTarea : System.Web.UI.Page
    {

        public static int virtualIdGenerator;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidateTaskFlowInfoExists();
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];

            if (!IsPostBack)
            {
                Session["TaskWithLevels"] = null;
                Session["repSubTask"] = null;
                LoadSubTaskInformation(taskFlowInfo);
            }
        }

        private void ValidateTaskFlowInfoExists()
        {
            if (Session["TaskFlowInfo"] == null)
            {
                Response.Redirect("CrearTarea.aspx");
            }
        }

        private void LoadSubTaskInformation(TaskFlowInfo taskFlowInfo)
        {
            LoadSubTaskDiv(taskFlowInfo);
            List<TaskWithLevel> taskList = (List<TaskWithLevel>)repSubTask.DataSource;
            LoadDivInformation(taskFlowInfo, false);
            ListInformationDataBind(taskFlowInfo.OriginalTask);

            if (taskFlowInfo.IsPredefined)
            {
                SetPredefinidedSubTaskInformation(taskList);
            }
            else
            {
                //LoadFirstTaskDiv();
            }
        }

        private void LoadDivInformation(TaskFlowInfo taskFlowInfo, bool haveBrothers)
        {
            if (taskFlowInfo.IsRepetitive)
            {
                EnableUniqueTaskControls(false, haveBrothers);
                LoadRepetitiveSubTaskDetailInformation(taskFlowInfo);
            }
            else
            {
                EnableUniqueTaskControls(true, haveBrothers);
            }
        }

        private void LoadEditInformation(TaskFlowInfo taskFlowInfo, TaskWithLevel selectedTask, List<TaskWithLevel> tasksWithLevels)
        {
            bool val = true;
            if (taskFlowInfo.IsRepetitive)
            {
                val = false;
                LoadRepetitiveSubTaskDetailInformation(taskFlowInfo);
            }

            EnableEditTaskControls(val, false, selectedTask, tasksWithLevels);
        }

        private void LoadRepetitiveSubTaskDetailInformation(TaskFlowInfo taskFlowInfo)
        {
            if (taskFlowInfo.IsDayOfWeek == true)
            {
                VisibilizeDayWeek(true);
            }
            else if (taskFlowInfo.IsDayOfWeek == false)
            {
                VisibilizeDayWeek(false);
            }
        }

        private void VisibilizeDayWeek(bool val)
        {
            for (int i = 0; i < repSubTask.Items.Count; i++)
            {
                RepeaterItem item = repSubTask.Items[i];
                EnableVisibleDiv(val, "divDiaSemana", item);
                EnableVisibleDiv(!val, "divDiaMes", item);
                EnableVisibleDiv(val, "divNumeroSemana", item);
                EnableVisibleDiv(true, "divMes", item);
            }
        }

        private void LoadSubTaskDiv(TaskFlowInfo taskFlowInfo)
        {
            List<TaskWithLevel> taskList = new List<TaskWithLevel>();

            if (taskFlowInfo.IsPredefined)
            {
                taskList.Add(new TaskWithLevel { Task = taskFlowInfo.OriginalTask, virtualId = taskFlowInfo.OriginalTask.Id });
                foreach (Task task in taskFlowInfo.OriginalTask.LoadChildTasks().Values.ToList())
                {
                    taskList.Add(new TaskWithLevel { Task = task, virtualId = task.Id });
                }
            }
            else
            {
                //taskList.Add(taskFlowInfo.OriginalTask);
            }
            repSubTask.DataSource = taskList;
            repSubTask.DataBind();
        }

        private void SetPredefinidedSubTaskInformation(List<TaskWithLevel> taskList)
        {
            for (int i = 0; i < repSubTask.Items.Count; i++)
            {
                RepeaterItem item = repSubTask.Items[i];

                TextBox txtName = (TextBox)item.FindControl("txtNombre");
                TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                Task task = taskList[i].Task;

                txtName.Text = task.Name;
                txtDescription.Text = task.Description;

                txtName.Enabled = false;
                txtDescription.Enabled = false;

                EnableCreateFlowButton(false, item);
                EnableDependenciesDiv(false, item);
            }
        }

        private void EnableCreateFlowButton(bool val, RepeaterItem item)
        {
            Button createFlow = (Button)item.FindControl("btnGuardar");
            createFlow.Visible = val;
        }

        private void EnableDependenciesDiv(bool val, RepeaterItem item)
        {
            EnableVisibleDiv(val, "divDependencia", item);
        }

        private void EnableUniqueTaskControls(bool val, bool haveBrothers)
        {
            for (int i = 0; i < repSubTask.Items.Count; i++)
            {
                RepeaterItem item = repSubTask.Items[i];
                EnableVisibleDiv(val, "divTareaUnica", item);
                EnableVisibleDiv(!val, "divTareaRepetitiva", item);
                EnableVisibleDiv(haveBrothers, "divDependencia", item);
            }
        }

        private void EnableEditTaskControls(bool val, bool haveBrothers, TaskWithLevel twl, List<TaskWithLevel> twlist)
        {
            for (int i = 0; i < repSubTask.Items.Count; i++)
            {
                RepeaterItem item = repSubTask.Items[i];
                EnableVisibleDiv(val, "divTareaUnica", item);
                EnableVisibleDiv(!val, "divTareaRepetitiva", item);
                EnableVisibleDiv(haveBrothers, "divDependencia", item);
                LoadEditInformation(val, item, twl);
            }
        }

        private void LoadDdlDependencia(TaskWithLevel twl, List<TaskWithLevel> twlist, RepeaterItem item)
        {
            DropDownList ddlDep = (DropDownList)item.FindControl("ddlTareaDependiente");
            Dictionary<int?, string> dict = GetBrotherTasks(twl, twlist);
            if (dict.Count > 0)
            {
                EnableDependenciesDiv(true, item);
                ddlDep.Visible = true;
            }
            else
            {
                EnableDependenciesDiv(false, item);
                ddlDep.Visible = false;
            }
            ddlDep.DataSource = dict;
            ddlDep.DataTextField = "Value";
            ddlDep.DataValueField = "Key";
            ddlDep.DataBind();
        }

        private void LoadEditInformation(bool val, RepeaterItem item, TaskWithLevel twl)
        {
            TextBox txtNombre = (TextBox)item.FindControl("txtNombre");
            TextBox txtDescripcion = (TextBox)item.FindControl("txtDescription");
            txtNombre.Text = twl.Task.Name;
            txtDescripcion.Text = twl.Task.Description;


            if (val)
            {
                TextBox txtInicio = (TextBox)item.FindControl("txtFechaInicio");
                TextBox txtFin = (TextBox)item.FindControl("txtFechaFin");
                txtInicio.Text = twl.Detail.Start != null ? twl.Detail.Start.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm") : "";
                txtFin.Text = twl.Detail.End.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            }


        }

        private void EnableVisibleDiv(bool val, string divName, RepeaterItem item)
        {
            if (val && divName == "divTareaUnica")
            {
                TextBox txtInicio = (TextBox)item.FindControl("txtFechaInicio");
                TextBox txtFin = (TextBox)item.FindControl("txtFechaFin");
                txtInicio.Text = DateTime.Now.ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
                txtFin.Text = DateTime.Now.AddDays(1).ToLocalTime().ToString("yyyy-MM-ddTHH:mm");
            }
            HtmlGenericControl div = (HtmlGenericControl)item.FindControl(divName);
            div.Visible = val;
        }

        private Dictionary<int?, string> GetBrotherTasks(TaskWithLevel twl, List<TaskWithLevel> twlist)
        {
            Dictionary<int?, string> brotherTasks = new Dictionary<int?, string>();
            brotherTasks.Add(-1, "Ninguna");
            foreach (TaskWithLevel tw in twlist.FindAll(x => twl.virtualId != x.virtualId && x.virtualParentId == twl.virtualParentId).ToList())
            {
                brotherTasks.Add(tw.virtualId, tw.Task.Name);
            }

            return brotherTasks;
        }

        private void ListInformationDataBind(Task task)
        {
            ChildTaskContainer ctc = new ChildTaskContainer(task);
            LoadTableDivs(ctc);
        }

        private TaskLevelDetail LoadFirstDetail()
        {
            TaskLevelDetail detail = new TaskLevelDetail();
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];
            detail.IsRepetitive = taskFlowInfo.IsRepetitive;

            if (taskFlowInfo.IsRepetitive)
            {
                detail.Start = taskFlowInfo.LoopTask.StartTime;
                detail.End = taskFlowInfo.LoopTask.EndTime;
                detail.IdMonth = taskFlowInfo.LoopTaskScheduleList[0].IdMonth;
                if (taskFlowInfo.IsDayOfWeek == true)
                {
                    List<string> dayOfWeekList = new List<string>();
                    List<string> NumberOfWeekList = new List<string>();
                    foreach (LoopTaskSchedule lts in taskFlowInfo.LoopTaskScheduleList)
                    {
                        dayOfWeekList.Add(lts.DayOfWeek.ToString());
                        NumberOfWeekList.Add(lts.NumberOfWeek.ToString());
                    }

                    detail.SelectedDaysOfWeek = dayOfWeekList;
                    detail.SelectedWeeks = NumberOfWeekList;
                }
                else if (taskFlowInfo.IsDayOfWeek == false)
                {
                    detail.SelectedDay = taskFlowInfo.LoopTaskScheduleList[0].DayOfMonth.ToString();
                }
            }
            else
            {
                detail.Start = taskFlowInfo.ProcessedTask.StartDate;
                detail.End = taskFlowInfo.ProcessedTask.EndDate;
            }

            return detail;
        }

        private void LoadTableDivs(ChildTaskContainer ctc)
        {
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];
            int rootLevel = 0;
            ctc.LoadLevel(rootLevel);
            List<TaskWithLevel> tasksWithLevels;

            if (Session["TaskWithLevels"] == null)
            {
                tasksWithLevels = new List<TaskWithLevel>();
                ctc.TransformToListPlainWithLevels(tasksWithLevels);
                tasksWithLevels[0].Detail = LoadFirstDetail();
                foreach (TaskWithLevel twl in tasksWithLevels)
                {
                    twl.virtualId = virtualIdGenerator++;
                }
            }
            else
            {
                tasksWithLevels = (List<TaskWithLevel>)Session["TaskWithLevels"];
            }


            /*
            if (taskFlowInfo.IsPredefined)
            {
                taskList = taskFlowInfo.OriginalTask.LoadChildTasks().Values.ToList();
            }
            else
            {
                taskList.Add(taskFlowInfo.OriginalTask);
            }
            */
            Session["TaskWithLevels"] = UpdateTableInformation(tasksWithLevels, null, 0);
        }

        private List<TaskWithLevel> UpdateTableInformation(List<TaskWithLevel> tasksWithLevels, string operation, int index)
        {
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];
            switch (operation)
            {
                case "Add":
                    TaskWithLevel parentTaskWithLevel = tasksWithLevels[index];

                    bool haveVirtualChilds = HaveVirtualChilds(parentTaskWithLevel.virtualId, tasksWithLevels);

                    int level = parentTaskWithLevel.Level + 1;
                    int operationIdAdd = 1;

                    TaskWithLevel twl = new TaskWithLevel()
                    {
                        virtualId = virtualIdGenerator++,
                        Level = level,
                        virtualParentId = parentTaskWithLevel.virtualId,
                        OperationId = operationIdAdd
                    };

                    List<TaskWithLevel> tempList = new List<TaskWithLevel>();
                    tempList.Add(twl);
                    repSubTask.DataSource = tempList;
                    repSubTask.DataBind();
                    Session["repSubTask"] = tempList;
                    LoadDivInformation(taskFlowInfo, haveVirtualChilds);
                    if (haveVirtualChilds)
                    {
                        LoadDdlDependencia(twl, tasksWithLevels, repSubTask.Items[0]);
                    }
                    break;

                case "Delete":
                    foreach (TaskWithLevel twlChild in tasksWithLevels.FindAll(x => x.virtualParentId == tasksWithLevels[index].virtualId))
                    {
                        tasksWithLevels.Remove(twlChild);
                    }
                    tasksWithLevels.Remove(tasksWithLevels[index]);
                    break;

                case "Update":

                    TaskWithLevel selectedTaskWithLevel = tasksWithLevels[index];
                    selectedTaskWithLevel.OperationId = 3;
                    List<TaskWithLevel> temp = new List<TaskWithLevel>();
                    temp.Add(selectedTaskWithLevel);
                    repSubTask.DataSource = temp;
                    repSubTask.DataBind();
                    Session["repSubTask"] = temp;
                    LoadEditInformation(taskFlowInfo, selectedTaskWithLevel, tasksWithLevels);

                    LoadDdlDependencia(selectedTaskWithLevel, tasksWithLevels, repSubTask.Items[0]);

                    break;

                case "Save":

                    int addOperation = 1;
                    int editOperation = 3;
                    TaskWithLevel stwl = ((List<TaskWithLevel>)Session["repSubTask"])[0];

                    int parentIndex;
                    if (stwl.virtualDependentid != null)
                    {
                        parentIndex = tasksWithLevels.FindIndex(x => x.virtualId == stwl.virtualDependentid);
                    }
                    else
                    {
                        parentIndex = tasksWithLevels.FindIndex(x => x.virtualId == stwl.virtualParentId);
                    }

                    if (stwl.OperationId == addOperation)
                    {
                        stwl = SaveOperation(stwl, taskFlowInfo, repSubTask.Items[0]);
                        tasksWithLevels.Insert(parentIndex + 1, stwl);
                    }
                    else if (stwl.OperationId == editOperation)
                    {
                        TaskWithLevel updateTwl = tasksWithLevels.Find(x => x.virtualId == stwl.virtualId);
                        updateTwl = SaveOperation(stwl, taskFlowInfo, repSubTask.Items[0]);
                    }
                    stwl.OperationId = 0;
                    break;
                default:
                    break;
            }


            repTabla.DataSource = tasksWithLevels;
            repTabla.DataBind();
            LoadTableInfo(tasksWithLevels);
            return tasksWithLevels;
        }



       /* private List<TaskWithLevel> SortByLevelAndDependants(List<TaskWithLevel> tasksWithLevels)
        {
            return tasksWithLevels.OrderBy(c => c.Level).ThenBy(x => x.virtualParentId).ToList();
        }*/

        private bool HaveVirtualChilds(int? virtualId, List<TaskWithLevel> twlist)
        {
            if (twlist.Count(x => x.virtualId != null && x.virtualParentId == virtualId) > 0)
            {
                return true;
            }
            return false;
        }

        private List<TaskWithLevel> GetTableDataSource()
        {
            return (List<TaskWithLevel>)Session["TaskWithLevels"];
        }



        private void LoadTableInfo(List<TaskWithLevel> tlwl)
        {
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];
            bool isOwnTask = !taskFlowInfo.IsPredefined;
            bool isUnique = !taskFlowInfo.IsRepetitive;
            EnableHeaderLabelForButtons(isOwnTask);

            for (int i = 0; i < repTabla.Items.Count; i++)
            {
                RepeaterItem item = repTabla.Items[i];
                TaskWithLevel twl = tlwl[i];
                //twl.Detail = new TaskLevelDetail();
                SetRowInTableInformation(item, twl);
                EnableSubButtons(item, isOwnTask, twl.Level);
                LoadUniqueFatherTaskInformation(item, taskFlowInfo, twl.Level);
                EnableUniqueHeaderInformation(isUnique);
            }
        }

        private void EnableUniqueHeaderInformation(bool val)
        {
            Label lblInformacionInicio = (Label)repTabla.Controls[0].Controls[0].FindControl("hdlblFechaInicio");
            Label lblInformacionFinHora = (Label)repTabla.Controls[0].Controls[0].FindControl("hdlblFechaFin");

            if (val)
            {
                lblInformacionInicio.Text = "Inicio";
                lblInformacionFinHora.Text = "Fin";
            }
            else
            {
                lblInformacionInicio.Text = "Repetir durante";
                lblInformacionFinHora.Text = "Horario";
            }
        }

        private void EnableHeaderLabelForButtons(bool val)
        {
            Label lblAdd = (Label)repTabla.Controls[0].Controls[0].FindControl("hdlblAgregar");
            Label lblDelete = (Label)repTabla.Controls[0].Controls[0].FindControl("hdlblEliminar");
            Label lblEdit = (Label)repTabla.Controls[0].Controls[0].FindControl("hdlblEditar");

            lblAdd.Visible = val;
            lblDelete.Visible = val;
            lblEdit.Visible = val;

        }


        private void EnableSubButtons(RepeaterItem item, bool val, int level)
        {
            SetTableIndividualButtonInformation("btnSubAdd", item, val);
            SetTableIndividualButtonInformation("btnSubEdit", item, val);

            if (level == 0)
            {
                SetTableIndividualButtonInformation("btnSubDelete", item, false);
            }
            else
            {
                SetTableIndividualButtonInformation("btnSubDelete", item, val);
            }
        }

        private void LoadUniqueFatherTaskInformation(RepeaterItem item, TaskFlowInfo taskFlowInfo, int level)
        {
            if (!taskFlowInfo.IsRepetitive && level == 0)
            {
                SetTableindividualLabelInformation("lblSubFechaInicio", taskFlowInfo.ProcessedTask.StartDate.ToString(), item);
                SetTableindividualLabelInformation("lblSubFechaFin", taskFlowInfo.ProcessedTask.EndDate.ToString(), item);
            }
        }

        private void SetRowInTableInformation(RepeaterItem item, TaskWithLevel taskWithLevel)
        {
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];
            List<TaskWithLevel> twl = (List<TaskWithLevel>)Session["TaskWithLevels"];

            Task task = taskWithLevel.Task;
            string separator = String.Concat(Enumerable.Repeat("---", taskWithLevel.Level));
            SetTableindividualLabelInformation("lblSubSeparator", separator, item);
            SetTableindividualLabelInformation("lblSubNombre", task.Name, item);
            SetTableindividualLabelInformation("lblSubDescripcion", task.Description, item);

            if (taskFlowInfo.IsPredefined && taskWithLevel.Detail == null)
            {
                SetTableindividualLabelInformation("lblSubFechaInicio", "Por definir", item);
                SetTableindividualLabelInformation("lblSubFechaFin", "Por definir", item);
            }
            else
            {
                if (taskFlowInfo.IsRepetitive)
                {
                    if (taskFlowInfo.IsDayOfWeek == true)
                    {
                        SetTableindividualLabelInformation("lblSubFechaInicio", SelectedDaysFormatter(taskWithLevel), item);
                    }
                    else if (taskFlowInfo.IsDayOfWeek == false)
                    {
                        SetTableindividualLabelInformation("lblSubFechaInicio", SelectedDayOfMonthFormatter(taskWithLevel), item);
                    }

                    SetTableindividualLabelInformation("lblSubFechaFin", SelectedHoursFormatter(taskWithLevel), item);
                }
                else
                {
                    SetTableindividualLabelInformation("lblSubFechaInicio", taskWithLevel.Detail.Start.ToString(), item);
                    SetTableindividualLabelInformation("lblSubFechaFin", taskWithLevel.Detail.End.ToString(), item);
                }
            }

            string depTaskName = string.Empty;

            if (task.DependentTask != null)
            {
                depTaskName = task.DependentTask.Name;
            }
            else if (taskWithLevel.virtualDependentid != null && taskWithLevel.virtualDependentid != -1)
            {
                depTaskName = twl.First(x => x.virtualId == taskWithLevel.virtualDependentid).Task.Name;
            }

            SetTableindividualLabelInformation("lblSubDependencia", depTaskName, item);
        }

        private string SelectedDaysFormatter(TaskWithLevel taskWithLevel)
        {
            string daysOfWeek = "días";
            Dictionary<int, string> dayOfWeek = ComboBoxDataLoader.DayOfWeek;

            if (taskWithLevel.Detail.SelectedDaysOfWeek != null)
            {
                List<string> days = taskWithLevel.Detail.SelectedDaysOfWeek.Distinct().ToList();

                if (days.Count > 0 && days.Count < 7)
                {
                    daysOfWeek = string.Empty;
                    for (int i = 0; i < days.Count; i++)
                    {
                        daysOfWeek += dayOfWeek[int.Parse(days[i])];

                        if (i < days.Count - 1)
                        {
                            daysOfWeek += ", ";
                        }
                    }
                }
            }

            string numberOfWeeks = string.Empty;
            if (taskWithLevel.Detail.SelectedDaysOfWeek != null && taskWithLevel.Detail.SelectedDaysOfWeek.Count > 0)
            {
                List<string> weeks = taskWithLevel.Detail.SelectedWeeks.Distinct().ToList();
                numberOfWeeks = "De la/s semana/s ";
                for (int i = 0; i < weeks.Count; i++)
                {
                    numberOfWeeks += weeks[i];
                    if (i < weeks.Count - 1)
                    {
                        numberOfWeeks += ", ";
                    }
                }
            }

            Dictionary<int, string> months = ComboBoxDataLoader.Month;
            int idMonth = taskWithLevel.Detail.IdMonth ?? 13;
            string month = months[idMonth];
            string returnValue = string.Format("Todos los {0} {1} durante {2}", daysOfWeek, numberOfWeeks, month);

            return returnValue;
        }

        private string SelectedDayOfMonthFormatter(TaskWithLevel taskWithLevel)
        {
            string dayOfMonth = taskWithLevel.Detail.SelectedDay;

            Dictionary<int, string> months = ComboBoxDataLoader.Month;
            int idMonth = taskWithLevel.Detail.IdMonth ?? 13;
            string month = months[idMonth];
            string returnValue = string.Format("Todos los {0} de {1}", dayOfMonth, month);

            return returnValue;
        }

        private string SelectedHoursFormatter(TaskWithLevel taskWithLevel)
        {
            DateTime datetime = (DateTime)taskWithLevel.Detail.Start;
            string startTime = datetime.ToString("HH:mm");
            string endTime = taskWithLevel.Detail.End.ToString("HH:mm");
            string returnValue = string.Format("De {0} a {1}", startTime, endTime);
            return returnValue;
        }

        private void SetTableIndividualButtonInformation(string buttonName, RepeaterItem item, bool val)
        {
            LinkButton button = (LinkButton)item.FindControl(buttonName);
            button.Enabled = val;
            button.Visible = val;
        }

        private void SetTableindividualLabelInformation(string labelName, string information, RepeaterItem item)
        {
            Label label = (Label)item.FindControl(labelName);
            label.Text = information;
        }

        protected void btnSubAdd_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            Session["TaskWithLevels"] = UpdateTableInformation(GetTableDataSource(), "Add", item.ItemIndex);
        }

        protected void btnSubDelete_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            Session["TaskWithLevels"] = UpdateTableInformation(GetTableDataSource(), "Delete", item.ItemIndex);
        }

        protected void btnSubEdit_Click(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            Session["TaskWithLevels"] = UpdateTableInformation(GetTableDataSource(), "Update", item.ItemIndex);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            RepeaterItem item = (RepeaterItem)button.NamingContainer;
            Session["TaskWithLevels"] = UpdateTableInformation(GetTableDataSource(), "Save", item.ItemIndex);
        }

        private TaskWithLevel SaveOperation(TaskWithLevel twl, TaskFlowInfo taskFlowInfo, RepeaterItem item)
        {
            int noDependantCode = -1;
            TextBox txtName = (TextBox)item.FindControl("txtNombre");
            TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
            twl.Task = new Task() { Name = txtName.Text, Description = txtDescription.Text };
            twl.Detail = new TaskLevelDetail();

            DropDownList ddlDep = (DropDownList)item.FindControl("ddlTareaDependiente");

            if (ddlDep.Visible && ddlDep.SelectedValue != noDependantCode.ToString())
            {
                twl.virtualDependentid = int.Parse(ddlDep.SelectedValue);
            }


            if (taskFlowInfo.IsRepetitive)
            {
                TextBox txtHoraInicio = (TextBox)item.FindControl("txtHoraInicio");
                TextBox txtHoraFin = (TextBox)item.FindControl("txtHoraFin");
                twl.Detail.LoadTimeForRepetitive(txtHoraInicio.Text, txtHoraFin.Text);
                DropDownList month = (DropDownList)item.FindControl("ddlMeses");
                twl.Detail.IdMonth = int.Parse(month.SelectedValue);

                if (taskFlowInfo.IsDayOfWeek == true)
                {

                    CheckBoxList cbxDiaSemana = (CheckBoxList)item.FindControl("cbxDiaSemana");
                    CheckBoxList cbxNumeroSemana = (CheckBoxList)item.FindControl("cbxNumeroSemana");

                    List<string> selectedDaysOfWeek = cbxDiaSemana.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
                    List<string> selectedWeeks = cbxNumeroSemana.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();

                    if (selectedWeeks.Distinct().Count() == 6)
                    {
                        selectedWeeks = null;
                    }

                    twl.Detail.SelectedDaysOfWeek = selectedDaysOfWeek;
                    twl.Detail.SelectedWeeks = selectedWeeks;
                }
                else if (taskFlowInfo.IsDayOfWeek == false)
                {
                    DropDownList ddlDiaDelMes = (DropDownList)item.FindControl("ddlDiaDelMes");
                    twl.Detail.SelectedDay = ddlDiaDelMes.SelectedValue;
                }
            }
            else if (!taskFlowInfo.IsRepetitive)
            {
                TextBox txtInicio = (TextBox)item.FindControl("txtFechaInicio");
                TextBox txtFin = (TextBox)item.FindControl("txtFechaFin");
                twl.Detail.Start = DateTime.Parse(txtInicio.Text);
                twl.Detail.End = DateTime.Parse(txtFin.Text);

                if (twl.OperationId == 3 && twl.Level == 0)
                {
                    taskFlowInfo.OriginalTask.Name = twl.Task.Name;
                    taskFlowInfo.OriginalTask.Description = twl.Task.Description;
                    taskFlowInfo.ProcessedTask.StartDate = (DateTime)twl.Detail.Start;
                    taskFlowInfo.ProcessedTask.EndDate = twl.Detail.End;
                    Session["TaskFlowInfo"] = taskFlowInfo;
                }
            }

            txtName.Text = "";
            txtDescription.Text = "";
            return twl;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearTarea.aspx");
        }

        protected void btnCrearFlujo_Click(object sender, EventArgs e)
        {
            TaskFlowInfo taskFlowInfo = (TaskFlowInfo)Session["TaskFlowInfo"];
            List<TaskWithLevel> twList = (List<TaskWithLevel>)Session["TaskWithLevels"];
            List<TaskWithLevel> rSubTask = (List<TaskWithLevel>)Session["repSubTask"];

            try
            {
                if (taskFlowInfo.IsPredefined)
                {
                    for (int i = 0; i < repSubTask.Items.Count; i++)
                    {
                        RepeaterItem item = repSubTask.Items[i];

                        Task task = twList[i].Task;
                        if (task.Create())
                        {
                            if (taskFlowInfo.IsRepetitive)
                            {
                                TaskAssignment taskAssignment = new TaskAssignment()
                                {
                                    AssignerUser = taskFlowInfo.LoopTask.TaskAssignment.AssignerUser,
                                    ReceiverUser = taskFlowInfo.LoopTask.TaskAssignment.ReceiverUser,
                                    Task = task
                                };
                                if (taskAssignment.Create())
                                {
                                    TextBox txtHoraInicio = (TextBox)item.FindControl("txtHoraInicio");
                                    TextBox txtHoraFin = (TextBox)item.FindControl("txtHoraFin");
                                    string defaultDate = "1900-01-01";
                                    DateTime startTime = DateTime.Parse(defaultDate + " " + txtHoraInicio.Text);
                                    DateTime endTime = DateTime.Parse(defaultDate + " " + txtHoraFin.Text);

                                    LoopTask loopTask = new LoopTask()
                                    {
                                        Isactive = true,
                                        TaskAssignment = taskAssignment,
                                        StartTime = startTime,
                                        EndTime = endTime
                                    };

                                    if (loopTask.Create())
                                    {
                                        List<LoopTaskSchedule> ltsList = new List<LoopTaskSchedule>();
                                        DropDownList ddlMonth = (DropDownList)item.FindControl("ddlMeses");
                                        int month = int.Parse(ddlMonth.SelectedValue);

                                        if (taskFlowInfo.IsDayOfWeek == true)
                                        {
                                            CheckBoxList cbxDiaSemana = (CheckBoxList)item.FindControl("cbxDiaSemana");
                                            CheckBoxList cbxNumeroSemana = (CheckBoxList)item.FindControl("cbxNumeroSemana");
                                            List<string> selectedDaysOfWeek = cbxDiaSemana.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
                                            List<string> selectedWeeks = cbxNumeroSemana.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();

                                            if (selectedWeeks.Count > 0 && selectedWeeks.Count < 6)
                                            {
                                                foreach (string dayOfWeek in selectedDaysOfWeek)
                                                {
                                                    foreach (string numberOfWeek in selectedWeeks)
                                                    {
                                                        LoopTaskSchedule lts = new LoopTaskSchedule()
                                                        {
                                                            LoopTask = loopTask,
                                                            DayOfMonth = null,
                                                            DayOfWeek = int.Parse(dayOfWeek),
                                                            NumberOfWeek = int.Parse(numberOfWeek),
                                                            IdMonth = month
                                                        };
                                                        ltsList.Add(lts);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (string dayOfWeek in selectedDaysOfWeek)
                                                {
                                                    LoopTaskSchedule lts = new LoopTaskSchedule()
                                                    {
                                                        LoopTask = loopTask,
                                                        DayOfMonth = null,
                                                        DayOfWeek = int.Parse(dayOfWeek),
                                                        NumberOfWeek = null,
                                                        IdMonth = month
                                                    };
                                                    ltsList.Add(lts);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (taskFlowInfo.IsDayOfWeek == false)
                                            {
                                                DropDownList ddlDiaDelMes = (DropDownList)item.FindControl("ddlDiaDelMes");
                                                string SelectedDay = ddlDiaDelMes.SelectedValue;
                                                LoopTaskSchedule lts = new LoopTaskSchedule()
                                                {
                                                    LoopTask = loopTask,
                                                    DayOfWeek = null,
                                                    NumberOfWeek = null,
                                                    DayOfMonth = int.Parse(SelectedDay),
                                                    IdMonth = month
                                                };
                                                ltsList.Add(lts);
                                            }
                                        }
                                        if (ltsList.Count > 0)
                                        {
                                            foreach (LoopTaskSchedule loopTaskSchedule in ltsList)
                                            {
                                                if (!loopTaskSchedule.Create())
                                                    throw new Exception();
                                                else
                                                    InsertAssignmentForThisWeek(loopTaskSchedule);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception();
                                    }
                                }
                                else
                                {
                                    throw new Exception();
                                }

                            }
                            else
                            {
                                int AssignedTaskStatusCode = 1;
                                TextBox txtInicio = (TextBox)item.FindControl("txtFechaInicio");
                                TextBox txtFin = (TextBox)item.FindControl("txtFechaFin");
                                TaskAssignment assignment = new TaskAssignment()
                                {
                                    Task = task,
                                    AssignerUser = taskFlowInfo.ProcessedTask.TaskAssignment.AssignerUser,
                                    ReceiverUser = taskFlowInfo.ProcessedTask.TaskAssignment.ReceiverUser
                                };

                                if (!assignment.Create())
                                    throw new Exception();

                                ProcessedTask processedTask = new ProcessedTask()
                                {
                                    AssignationDate = DateTime.Now,
                                    Commentary = null,
                                    StartDate = DateTime.Parse(txtInicio.Text),
                                    EndDate = DateTime.Parse(txtFin.Text),
                                    LoopTaskSchedule = null,
                                    TaskAssignment = assignment,
                                    IdTaskStatus = AssignedTaskStatusCode
                                };

                                if (!processedTask.Create())
                                {
                                    throw new Exception();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
                else
                {
                    foreach (TaskWithLevel twl in twList)
                    {
                        if (twl.Level == 0 && twl.virtualParentId == null)
                        {
                            twl.Task.SuperiorTask = null;
                            twl.Task.DependentTask = null;
                        }
                        else
                        {
                            if (twl.virtualParentId != null)
                            {
                                int parentTaskId = twList.First(x => x.virtualId == twl.virtualParentId).Task.Id;
                                twl.Task.SuperiorTask = new Task(parentTaskId);
                            }
                            if (twl.virtualDependentid != null)
                            {
                                int dependentTaskId = twList.First(x => x.virtualId == twl.virtualDependentid).Task.Id;
                                twl.Task.DependentTask = new Task(dependentTaskId);
                            }
                        }

                        if (!twl.Task.Create())
                            throw new Exception();


                        if (taskFlowInfo.IsRepetitive)
                        {
                            TaskAssignment taskAssignment = new TaskAssignment()
                            {
                                Task = twl.Task,
                                AssignerUser = taskFlowInfo.LoopTask.TaskAssignment.AssignerUser,
                                ReceiverUser = taskFlowInfo.LoopTask.TaskAssignment.ReceiverUser
                            };

                            if (!taskAssignment.Create())
                            {
                                throw new Exception();
                            }

                            LoopTask loopt = new LoopTask
                            {
                                Isactive = true,
                                TaskAssignment = taskAssignment,
                                StartTime = twl.Detail.Start,
                                EndTime = twl.Detail.End
                            };

                            if (!loopt.Create())
                            {
                                throw new Exception();
                            }

                            List<LoopTaskSchedule> ltsList = new List<LoopTaskSchedule>();
                            int idMonth = (int)twl.Detail.IdMonth;
                            if (taskFlowInfo.IsDayOfWeek == true)
                            {
                                List<string> selectedDaysOfWeek = twl.Detail.SelectedDaysOfWeek;
                                List<string> selectedWeeks = twl.Detail.SelectedWeeks;

                                if (selectedWeeks.Count > 0 && selectedWeeks.Count < 6)
                                {
                                    foreach (string dayOfWeek in selectedDaysOfWeek)
                                    {
                                        foreach (string numberOfWeek in selectedWeeks)
                                        {
                                            LoopTaskSchedule ltsch = new LoopTaskSchedule()
                                            {
                                                LoopTask = null,
                                                DayOfMonth = null,
                                                DayOfWeek = int.Parse(dayOfWeek),
                                                NumberOfWeek = int.Parse(numberOfWeek),
                                                IdMonth = idMonth
                                            };
                                            ltsList.Add(ltsch);
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (string dayOfWeek in selectedDaysOfWeek)
                                    {
                                        LoopTaskSchedule ltsch = new LoopTaskSchedule()
                                        {
                                            LoopTask = null,
                                            DayOfMonth = null,
                                            DayOfWeek = int.Parse(dayOfWeek),
                                            NumberOfWeek = null,
                                            IdMonth = idMonth
                                        };

                                        ltsList.Add(ltsch);
                                    }
                                }
                            }
                            else if (taskFlowInfo.IsDayOfWeek == false)
                            {
                                int selectedDay = int.Parse(twl.Detail.SelectedDay);
                                LoopTaskSchedule ltsch = new LoopTaskSchedule()
                                {
                                    LoopTask = null,
                                    DayOfWeek = null,
                                    NumberOfWeek = null,
                                    DayOfMonth = selectedDay,
                                    IdMonth = idMonth
                                };
                                ltsList.Add(ltsch);
                            }
                            if (ltsList.Count > 0)
                            {
                                foreach (LoopTaskSchedule loopTaskSchedule in ltsList)
                                {
                                    if (!loopTaskSchedule.Create())
                                        throw new Exception();
                                    else
                                        InsertAssignmentForThisWeek(loopTaskSchedule);
                                }
                            }
                            LoopTaskSchedule lts = new LoopTaskSchedule
                            {

                                LoopTask = loopt,
                            };
                        }
                        else
                        {
                            TaskAssignment taskAssignment = new TaskAssignment()
                            {
                                Task = twl.Task,
                                AssignerUser = taskFlowInfo.ProcessedTask.TaskAssignment.AssignerUser,
                                ReceiverUser = taskFlowInfo.ProcessedTask.TaskAssignment.ReceiverUser
                            };

                            if (taskAssignment.Create())
                            {
                                int assignedTaskStatusCode = 1;
                                ProcessedTask prt = new ProcessedTask()
                                {
                                    AssignationDate = DateTime.Now,
                                    Commentary = null,
                                    StartDate = (DateTime)twl.Detail.Start,
                                    EndDate = twl.Detail.End,
                                    LoopTaskSchedule = null,
                                    TaskAssignment = taskAssignment,
                                    IdTaskStatus = assignedTaskStatusCode
                                };
                                prt.Create();
                            }
                            else
                            {
                                throw new Exception();
                            }
                        }
                    }
                }
                Response.Redirect("CreacionTareaExitosa.aspx", false);
            }
            catch (Exception exc)
            {
                Response.Redirect("FlujodeTarea.aspx", false);
            }
        }

        /// <summary>
        /// Inserción de datos de repetitivas
        /// </summary>
        /// <param name="loopTaskSchedule"></param>
        private void InsertAssignmentForThisWeek(LoopTaskSchedule loopTaskSchedule)
        {
            int? selectedDayOfWeek = loopTaskSchedule.DayOfWeek;
            int? dayOfMonth = loopTaskSchedule.DayOfMonth;
            int? idMonth = loopTaskSchedule.IdMonth;
            int? numberOfWeek = loopTaskSchedule.NumberOfWeek;
            DateTime now = DateTime.Now;
            DateTime startTime = (DateTime)loopTaskSchedule.LoopTask.StartTime;
            DateTime endTime = loopTaskSchedule.LoopTask.EndTime;
            int actualDayOfWeek = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)DateTime.Today.DayOfWeek;

            if (idMonth == 13 || MonthBelongsToActualWeek(idMonth))
            {
                if (dayOfMonth == null)
                {
                    if ((numberOfWeek == null || numberOfWeek == GetWeekNumberOfMonth(now)) && selectedDayOfWeek > actualDayOfWeek || (selectedDayOfWeek == actualDayOfWeek && TimeSpan.Compare(startTime.TimeOfDay, now.TimeOfDay) > 0))
                    {
                        int differenceBetweenDays = ((int)selectedDayOfWeek) - actualDayOfWeek;
                        CreateProcessedTask(loopTaskSchedule, startTime, endTime, differenceBetweenDays);
                        //int totalDaysOfWeek = 7;
                        //differenceBetweenDays = totalDaysOfWeek - actualDayOfWeek;
                        //if (TimeSpan.Compare(startTime.TimeOfDay, now.TimeOfDay) > 0)
                        //{
                        //    actualDayOfWeek++;
                        //}

                        //while (actualDayOfWeek < differenceBetweenDays)
                        //{
                        //    CreateProcessedTask(loopTaskSchedule, startTime, endTime, differenceBetweenDays);
                        //    actualDayOfWeek++;
                        //}
                    }
                }
                else
                {
                    DateTime firstDayOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                    DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
                    int day = (int)dayOfMonth;
                    DateTime wishedDay = new DateTime();
                    if (firstDayOfWeek.Month == lastDayOfWeek.Month)
                    {
                        try
                        {
                            wishedDay = new DateTime(now.Year, now.Month, day);
                        }
                        catch (Exception e) { }
                    }
                    else if ((firstDayOfWeek.Month < lastDayOfWeek.Month) || (firstDayOfWeek.Month > lastDayOfWeek.Month && firstDayOfWeek.Year != lastDayOfWeek.Year))
                    {
                        try
                        {
                            if (day > firstDayOfWeek.Day)
                            {
                                wishedDay = new DateTime(now.Year, firstDayOfWeek.Month, day);
                            }
                            else if (day <= lastDayOfWeek.Day)
                            {
                                wishedDay = new DateTime(now.Year, lastDayOfWeek.Month, day);
                            }
                        }
                        catch (Exception e) { }
                    }

                    if (wishedDay >= firstDayOfWeek && wishedDay <= lastDayOfWeek)
                    {
                        DateTime startDate = wishedDay.Date + startTime.TimeOfDay;
                        DateTime endDate = wishedDay.Date + endTime.TimeOfDay;
                        CreateProcessedTask(loopTaskSchedule, startDate, endDate);
                    }
                }
            }
        }

        private bool CreateProcessedTask(LoopTaskSchedule loopTaskSchedule, DateTime startDate, DateTime endDate)
        {
            int assignedStatusId = 1;
            ProcessedTask processedTask = new ProcessedTask()
            {
                Commentary = null,
                AssignationDate = DateTime.Now,
                IdTaskStatus = assignedStatusId,
                TaskAssignment = loopTaskSchedule.LoopTask.TaskAssignment,
                LoopTaskSchedule = loopTaskSchedule,
                StartDate = startDate,
                EndDate = endDate
            };
            return processedTask.Create();
        }

        private bool CreateProcessedTask(LoopTaskSchedule loopTaskSchedule, DateTime startTime, DateTime endTime, int differenceBetweenDays)
        {
            int assignedStatusId = 1;
            DateTime startDate = DateTime.Now.AddDays(differenceBetweenDays);
            startDate = startDate.Date + startTime.TimeOfDay;
            DateTime endDate = startDate.Date + endTime.TimeOfDay;
            ProcessedTask processedTask = new ProcessedTask()
            {
                Commentary = null,
                AssignationDate = DateTime.Now,
                IdTaskStatus = assignedStatusId,
                TaskAssignment = loopTaskSchedule.LoopTask.TaskAssignment,
                LoopTaskSchedule = loopTaskSchedule,
                StartDate = startDate,
                EndDate = endDate
            };
            return processedTask.Create();
        }

        private bool MonthBelongsToActualWeek(int? idMonth)
        {
            bool val = false;

            if (idMonth != 13)
            {
                DateTime firstDayOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
                if (firstDayOfWeek.Month == idMonth || lastDayOfWeek.Month == idMonth)
                {
                    val = true;
                }
            }

            return val;
        }


        private int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            int value = ((date - firstMonthMonday).Days / 7 + 1) + 1;
            return value;
        }

        /*
        private string Validate()
        {
            string value = null;

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtNombreTarea.Text))
            {
                value = "Error: Verifique que el nombre de la tarea y la descripción no esté vacío";
            }

            if (rbtlTipoTarea.SelectedItem.Value == "TareaUnica")
            {
                DateTime datetimeInicio;
                DateTime datetimeFin;
                string fechaInicio = txtFechaInicio.Text;
                string fechaFin = txtFechaFin.Text;

                if (string.IsNullOrWhiteSpace(fechaInicio) || string.IsNullOrWhiteSpace(fechaFin) || !DateTime.TryParse(fechaInicio, out datetimeInicio) || !DateTime.TryParse(fechaFin, out datetimeFin))
                {
                    value = "Las fechas ingresadas son inválidas, o bien, estas vienen vacías";
                }
                else
                {
                    if (datetimeInicio > datetimeFin)
                    {
                        value = "La fecha de inicio no puede ser mayor que la fecha de fin";
                    }
                }
            }
            else if (rbtlTipoTarea.SelectedItem.Value == "TareaRepetitiva")
            {
                DateTime horaInicio;
                DateTime horaFin;
                string horaInicial = txtHoraInicio.Text;
                string horaFinal = txtHoraFin.Text;
                string initialDay = "1900-01-01";
                if (string.IsNullOrWhiteSpace(horaInicial) || string.IsNullOrWhiteSpace(horaFinal) || !DateTime.TryParse(initialDay + " " + horaInicial, out horaInicio) || !DateTime.TryParse(initialDay + " " + horaFinal, out horaFin))
                {
                    value = "Hora de inicio y fin no pueden ser vacíos, o bien los datos ingresados son inválidos";
                }
                else
                {
                    if (horaInicio > horaFin)
                    {
                        value = "La hora de inicio no puede ser mayor que la hora de fin";
                    }
                }

                if (rbtlTipoRepeticion.SelectedValue == "diaSemana")
                {
                    if (cbxDiaSemana.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList().Count == 0)
                    {
                        value = "Debe marcar al menos 1 día de semana";
                    }
                }
            }

            return value;
        }
*/
        /*
        private void ConfigureTreeView(ChildTaskContainer ctc)
        {
            
            TreeNode originalTaskNode = new TreeNode();

            originalTaskNode.Text = ctc.Task.Name;
            originalTaskNode.Value = ctc.Task.Id.ToString();
            trvTarea.Nodes.Add(originalTaskNode);
            if (ctc.ChildTasks != null)
            {
                foreach(ChildTaskContainer ctchild in ctc.ChildTasks)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = ctchild.Task.Name;
                    childNode.Value = ctc.Task.Id.ToString();
                    originalTaskNode.ChildNodes.Add(childNode);
                    ConfigureTreeView(ctchild, childNode);
                }
            }
        }

        private void ConfigureTreeView(ChildTaskContainer ctc, TreeNode node)
        {
            if (ctc.ChildTasks != null)
            {
                foreach (ChildTaskContainer ctchild in ctc.ChildTasks)
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = ctchild.Task.Name;
                    childNode.Value = ctc.Task.Id.ToString();
                    node.ChildNodes.Add(childNode);
                    ConfigureTreeView(ctchild, childNode);
                }
            }
        }
        */

        /* private void TreeListViewTest ()
         {
             TreeListView tlv = new TreeListView();
             tlv.add

         }*/
        /*
         * foreach (RepeaterItem item in Repeater1.Items)
            {
                  TextBox txtName= (TextBox)item.FindControl("txtName");
                  if(txtName!=null)
                  {
                  //do something with txtName.Text
                  }
                  Image img= (Image)item.FindControl("Img");
                  if(img!=null)
                  {
                  //do something with img
                  }
            }
         */
    }
}