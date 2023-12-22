namespace NeuralNetwork1
{
    partial class NeuralNetworksStand
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            this.вапрвапрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.button_F = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.st_lable = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.createDataset = new System.Windows.Forms.Button();
            this.LoadDataset = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.netTypeBox = new System.Windows.Forms.ComboBox();
            this.parallelCheckBox = new System.Windows.Forms.CheckBox();
            this.netStructureBox = new System.Windows.Forms.TextBox();
            this.recreateNetButton = new System.Windows.Forms.Button();
            this.classCounter = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.testNetButton = new System.Windows.Forms.Button();
            this.netTrainButton = new System.Windows.Forms.Button();
            this.AccuracyCounter = new System.Windows.Forms.TrackBar();
            this.EpochesCounter = new System.Windows.Forms.NumericUpDown();
            this.TrainingSizeCounter = new System.Windows.Forms.NumericUpDown();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_aiml_request = new System.Windows.Forms.TextBox();
            this.bt_aiml_send = new System.Windows.Forms.Button();
            this.tb_aiml_answer = new System.Windows.Forms.TextBox();
            this.tb_tlbot_answer = new System.Windows.Forms.TextBox();
            this.bt_enable_bot = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            this.controlPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tresholdTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccuracyCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpochesCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingSizeCounter)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(77, 47);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(84, 13);
            label2.TabIndex = 2;
            label2.Text = "Структура сети";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 73);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(153, 13);
            label4.TabIndex = 5;
            label4.Text = "Размер обучающей выборки";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(69, 100);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(92, 13);
            label5.TabIndex = 7;
            label5.Text = "Количество эпох";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(23, 194);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(54, 13);
            label6.TabIndex = 9;
            label6.Text = "Точность";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(377, 326);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(40, 13);
            label7.TabIndex = 14;
            label7.Text = "Status:";
            // 
            // вапрвапрToolStripMenuItem
            // 
            this.вапрвапрToolStripMenuItem.Name = "вапрвапрToolStripMenuItem";
            this.вапрвапрToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.вапрвапрToolStripMenuItem.Text = "вапрвапр";
            // 
            // controlPanel
            // 
            this.controlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.controlPanel.Controls.Add(this.button_F);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(11, 464);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(179, 59);
            this.controlPanel.TabIndex = 43;
            // 
            // button_F
            // 
            this.button_F.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_F.Location = new System.Drawing.Point(10, 13);
            this.button_F.Name = "button_F";
            this.button_F.Size = new System.Drawing.Size(156, 30);
            this.button_F.TabIndex = 2;
            this.button_F.Text = "Распознать";
            this.button_F.UseVisualStyleBackColor = true;
            this.button_F.Click += new System.EventHandler(this.button_F_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tresholdTrackBar);
            this.panel2.Location = new System.Drawing.Point(11, 373);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 85);
            this.panel2.TabIndex = 39;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(145, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 24;
            this.checkBox1.Text = "Обработать";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Порог";
            // 
            // tresholdTrackBar
            // 
            this.tresholdTrackBar.LargeChange = 1;
            this.tresholdTrackBar.Location = new System.Drawing.Point(-1, 31);
            this.tresholdTrackBar.Maximum = 255;
            this.tresholdTrackBar.Name = "tresholdTrackBar";
            this.tresholdTrackBar.Size = new System.Drawing.Size(140, 45);
            this.tresholdTrackBar.TabIndex = 22;
            this.tresholdTrackBar.TickFrequency = 25;
            this.tresholdTrackBar.Value = 35;
            this.tresholdTrackBar.ValueChanged += new System.EventHandler(this.tresholdTrackBar_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(11, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // st_lable
            // 
            this.st_lable.AutoSize = true;
            this.st_lable.Location = new System.Drawing.Point(423, 326);
            this.st_lable.Name = "st_lable";
            this.st_lable.Size = new System.Drawing.Size(38, 13);
            this.st_lable.TabIndex = 15;
            this.st_lable.Text = "NONE";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.createDataset);
            this.groupBox1.Controls.Add(this.LoadDataset);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.netTypeBox);
            this.groupBox1.Controls.Add(this.parallelCheckBox);
            this.groupBox1.Controls.Add(this.netStructureBox);
            this.groupBox1.Controls.Add(this.recreateNetButton);
            this.groupBox1.Controls.Add(this.classCounter);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.testNetButton);
            this.groupBox1.Controls.Add(this.netTrainButton);
            this.groupBox1.Controls.Add(this.AccuracyCounter);
            this.groupBox1.Controls.Add(label6);
            this.groupBox1.Controls.Add(this.EpochesCounter);
            this.groupBox1.Controls.Add(label5);
            this.groupBox1.Controls.Add(this.TrainingSizeCounter);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Location = new System.Drawing.Point(375, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(421, 299);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры сети";
            // 
            // createDataset
            // 
            this.createDataset.Location = new System.Drawing.Point(292, 17);
            this.createDataset.Margin = new System.Windows.Forms.Padding(2);
            this.createDataset.Name = "createDataset";
            this.createDataset.Size = new System.Drawing.Size(117, 27);
            this.createDataset.TabIndex = 23;
            this.createDataset.Text = "Создать Dataset";
            this.createDataset.UseVisualStyleBackColor = true;
            this.createDataset.Click += new System.EventHandler(this.createDataset_Click);
            // 
            // LoadDataset
            // 
            this.LoadDataset.Location = new System.Drawing.Point(292, 48);
            this.LoadDataset.Margin = new System.Windows.Forms.Padding(2);
            this.LoadDataset.Name = "LoadDataset";
            this.LoadDataset.Size = new System.Drawing.Size(117, 24);
            this.LoadDataset.TabIndex = 22;
            this.LoadDataset.Text = "Загрузить Dataset";
            this.LoadDataset.UseVisualStyleBackColor = true;
            this.LoadDataset.Click += new System.EventHandler(this.LoadDataset_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(129, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Сеть";
            // 
            // netTypeBox
            // 
            this.netTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.netTypeBox.FormattingEnabled = true;
            this.netTypeBox.Location = new System.Drawing.Point(166, 19);
            this.netTypeBox.Name = "netTypeBox";
            this.netTypeBox.Size = new System.Drawing.Size(121, 21);
            this.netTypeBox.TabIndex = 20;
            // 
            // parallelCheckBox
            // 
            this.parallelCheckBox.AutoSize = true;
            this.parallelCheckBox.Checked = true;
            this.parallelCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.parallelCheckBox.Location = new System.Drawing.Point(34, 243);
            this.parallelCheckBox.Name = "parallelCheckBox";
            this.parallelCheckBox.Size = new System.Drawing.Size(139, 17);
            this.parallelCheckBox.TabIndex = 19;
            this.parallelCheckBox.Text = "Параллельный расчёт";
            this.parallelCheckBox.UseVisualStyleBackColor = true;
            // 
            // netStructureBox
            // 
            this.netStructureBox.Location = new System.Drawing.Point(167, 44);
            this.netStructureBox.Name = "netStructureBox";
            this.netStructureBox.Size = new System.Drawing.Size(120, 20);
            this.netStructureBox.TabIndex = 18;
            this.netStructureBox.Text = "96;500;50;2";
            // 
            // recreateNetButton
            // 
            this.recreateNetButton.Location = new System.Drawing.Point(77, 161);
            this.recreateNetButton.Name = "recreateNetButton";
            this.recreateNetButton.Size = new System.Drawing.Size(140, 30);
            this.recreateNetButton.TabIndex = 17;
            this.recreateNetButton.Text = "Пересоздать сеть";
            this.recreateNetButton.UseVisualStyleBackColor = true;
            this.recreateNetButton.Click += new System.EventHandler(this.button3_Click);
            this.recreateNetButton.MouseEnter += new System.EventHandler(this.recreateNetButton_MouseEnter);
            // 
            // classCounter
            // 
            this.classCounter.Location = new System.Drawing.Point(167, 125);
            this.classCounter.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.classCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.classCounter.Name = "classCounter";
            this.classCounter.Size = new System.Drawing.Size(120, 20);
            this.classCounter.TabIndex = 16;
            this.classCounter.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.classCounter.ValueChanged += new System.EventHandler(this.classCounter_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(111, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Количество классов";
            // 
            // testNetButton
            // 
            this.testNetButton.Location = new System.Drawing.Point(163, 261);
            this.testNetButton.Name = "testNetButton";
            this.testNetButton.Size = new System.Drawing.Size(100, 30);
            this.testNetButton.TabIndex = 14;
            this.testNetButton.Text = "Тест";
            this.testNetButton.UseVisualStyleBackColor = true;
            this.testNetButton.Click += new System.EventHandler(this.button2_Click);
            this.testNetButton.MouseEnter += new System.EventHandler(this.testNetButton_MouseEnter);
            // 
            // netTrainButton
            // 
            this.netTrainButton.Location = new System.Drawing.Point(31, 261);
            this.netTrainButton.Name = "netTrainButton";
            this.netTrainButton.Size = new System.Drawing.Size(100, 30);
            this.netTrainButton.TabIndex = 11;
            this.netTrainButton.Text = "Обучить";
            this.netTrainButton.UseVisualStyleBackColor = true;
            this.netTrainButton.Click += new System.EventHandler(this.button1_Click);
            this.netTrainButton.MouseEnter += new System.EventHandler(this.netTrainButton_MouseEnter);
            // 
            // AccuracyCounter
            // 
            this.AccuracyCounter.Location = new System.Drawing.Point(25, 210);
            this.AccuracyCounter.Maximum = 100;
            this.AccuracyCounter.Name = "AccuracyCounter";
            this.AccuracyCounter.Size = new System.Drawing.Size(245, 45);
            this.AccuracyCounter.TabIndex = 10;
            this.AccuracyCounter.TickFrequency = 10;
            this.AccuracyCounter.Value = 90;
            // 
            // EpochesCounter
            // 
            this.EpochesCounter.Location = new System.Drawing.Point(167, 98);
            this.EpochesCounter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.EpochesCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EpochesCounter.Name = "EpochesCounter";
            this.EpochesCounter.Size = new System.Drawing.Size(120, 20);
            this.EpochesCounter.TabIndex = 8;
            this.EpochesCounter.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // TrainingSizeCounter
            // 
            this.TrainingSizeCounter.Location = new System.Drawing.Point(167, 71);
            this.TrainingSizeCounter.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.TrainingSizeCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TrainingSizeCounter.Name = "TrainingSizeCounter";
            this.TrainingSizeCounter.Size = new System.Drawing.Size(120, 20);
            this.TrainingSizeCounter.TabIndex = 6;
            this.TrainingSizeCounter.Value = new decimal(new int[] {
            700,
            0,
            0,
            0});
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.Location = new System.Drawing.Point(383, 367);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(43, 13);
            this.elapsedTimeLabel.TabIndex = 12;
            this.elapsedTimeLabel.Text = "Время:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите сеть, обучите или протестируйте";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(380, 342);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(291, 22);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(406, 393);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(383, 393);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 130);
            this.label9.TabIndex = 7;
            this.label9.Text = "0:\r\n1:\r\n2:\r\n3:\r\n4:\r\n5:\r\n6:\r\n7:\r\n8:\r\n9:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_aiml_answer);
            this.groupBox2.Controls.Add(this.bt_aiml_send);
            this.groupBox2.Controls.Add(this.tb_aiml_request);
            this.groupBox2.Location = new System.Drawing.Point(803, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 180);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AIML чат-бот";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bt_enable_bot);
            this.groupBox3.Controls.Add(this.tb_tlbot_answer);
            this.groupBox3.Location = new System.Drawing.Point(803, 210);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 313);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Telegram";
            // 
            // tb_aiml_request
            // 
            this.tb_aiml_request.Location = new System.Drawing.Point(6, 24);
            this.tb_aiml_request.Name = "tb_aiml_request";
            this.tb_aiml_request.Size = new System.Drawing.Size(314, 20);
            this.tb_aiml_request.TabIndex = 0;
            // 
            // bt_aiml_send
            // 
            this.bt_aiml_send.Location = new System.Drawing.Point(219, 47);
            this.bt_aiml_send.Name = "bt_aiml_send";
            this.bt_aiml_send.Size = new System.Drawing.Size(101, 25);
            this.bt_aiml_send.TabIndex = 1;
            this.bt_aiml_send.Text = "Обработать";
            this.bt_aiml_send.UseVisualStyleBackColor = true;
            // 
            // tb_aiml_answer
            // 
            this.tb_aiml_answer.Location = new System.Drawing.Point(6, 78);
            this.tb_aiml_answer.Multiline = true;
            this.tb_aiml_answer.Name = "tb_aiml_answer";
            this.tb_aiml_answer.Size = new System.Drawing.Size(314, 96);
            this.tb_aiml_answer.TabIndex = 2;
            // 
            // tb_tlbot_answer
            // 
            this.tb_tlbot_answer.Location = new System.Drawing.Point(6, 48);
            this.tb_tlbot_answer.Multiline = true;
            this.tb_tlbot_answer.Name = "tb_tlbot_answer";
            this.tb_tlbot_answer.Size = new System.Drawing.Size(314, 256);
            this.tb_tlbot_answer.TabIndex = 0;
            // 
            // bt_enable_bot
            // 
            this.bt_enable_bot.Location = new System.Drawing.Point(219, 19);
            this.bt_enable_bot.Name = "bt_enable_bot";
            this.bt_enable_bot.Size = new System.Drawing.Size(101, 23);
            this.bt_enable_bot.TabIndex = 1;
            this.bt_enable_bot.Text = "Включить бота";
            this.bt_enable_bot.UseVisualStyleBackColor = true;
            // 
            // NeuralNetworksStand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 544);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.elapsedTimeLabel);
            this.Controls.Add(this.st_lable);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "NeuralNetworksStand";
            this.Text = "Продвинутый студенческий перспетрон";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.controlPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tresholdTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccuracyCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EpochesCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrainingSizeCounter)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem вапрвапрToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button button_F;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tresholdTrackBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label st_lable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button createDataset;
        private System.Windows.Forms.Button LoadDataset;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox netTypeBox;
        private System.Windows.Forms.CheckBox parallelCheckBox;
        private System.Windows.Forms.TextBox netStructureBox;
        private System.Windows.Forms.Button recreateNetButton;
        private System.Windows.Forms.NumericUpDown classCounter;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button testNetButton;
        private System.Windows.Forms.Button netTrainButton;
        private System.Windows.Forms.TrackBar AccuracyCounter;
        private System.Windows.Forms.NumericUpDown EpochesCounter;
        private System.Windows.Forms.NumericUpDown TrainingSizeCounter;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_aiml_answer;
        private System.Windows.Forms.Button bt_aiml_send;
        private System.Windows.Forms.TextBox tb_aiml_request;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tb_tlbot_answer;
        private System.Windows.Forms.Button bt_enable_bot;
    }
}

