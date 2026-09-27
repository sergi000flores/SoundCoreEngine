namespace SoundCore.UI
{
  partial class FormResultadoBenchmark
  {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
      this.lblEncabezado = new System.Windows.Forms.Label();
      this.dgvResultados = new System.Windows.Forms.DataGridView();
      this.lblConclusion = new System.Windows.Forms.Label();
      this.btnCerrar = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
      this.SuspendLayout();
      //
      // lblEncabezado
      //
      this.lblEncabezado.AutoSize = true;
      this.lblEncabezado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
      this.lblEncabezado.Location = new System.Drawing.Point(15, 15);
      this.lblEncabezado.Name = "lblEncabezado";
      this.lblEncabezado.Size = new System.Drawing.Size(300, 20);
      this.lblEncabezado.TabIndex = 0;
      this.lblEncabezado.Text = "📊 Tiempo promedio por inserción según el tamaño de la cola";
      //
      // dgvResultados
      //
      this.dgvResultados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvResultados.AllowUserToAddRows = false;
      this.dgvResultados.AllowUserToDeleteRows = false;
      this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvResultados.Location = new System.Drawing.Point(15, 45);
      this.dgvResultados.MultiSelect = false;
      this.dgvResultados.Name = "dgvResultados";
      this.dgvResultados.ReadOnly = true;
      this.dgvResultados.RowHeadersWidth = 25;
      this.dgvResultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvResultados.Size = new System.Drawing.Size(730, 210);
      this.dgvResultados.TabIndex = 1;
      //
      // lblConclusion
      //
      this.lblConclusion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lblConclusion.Location = new System.Drawing.Point(15, 265);
      this.lblConclusion.Name = "lblConclusion";
      this.lblConclusion.Size = new System.Drawing.Size(730, 160);
      this.lblConclusion.TabIndex = 2;
      this.lblConclusion.Text = "";
      //
      // btnCerrar
      //
      this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCerrar.Location = new System.Drawing.Point(655, 435);
      this.btnCerrar.Name = "btnCerrar";
      this.btnCerrar.Size = new System.Drawing.Size(90, 30);
      this.btnCerrar.TabIndex = 3;
      this.btnCerrar.Text = "Cerrar";
      this.btnCerrar.UseVisualStyleBackColor = true;
      this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
      //
      // FormResultadoBenchmark
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(760, 480);
      this.Controls.Add(this.btnCerrar);
      this.Controls.Add(this.lblConclusion);
      this.Controls.Add(this.dgvResultados);
      this.Controls.Add(this.lblEncabezado);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(650, 420);
      this.Name = "FormResultadoBenchmark";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Resultado del Benchmark de Inserción";
      ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label lblEncabezado;
    private System.Windows.Forms.DataGridView dgvResultados;
    private System.Windows.Forms.Label lblConclusion;
    private System.Windows.Forms.Button btnCerrar;
  }
}
