/*******************************************************************************
   Author Information:
   Name: Victor V. Vu
   Email: vuvictor@csu.fullerton.edu

   Program Information:
   Program Name: Mouse Chase
   This File: CatnMouseui.cs
   Description: UI file containing animations & controls for the mouse game

   Copyright (C) 2022 Victor V. Vu
   This program is free software: you can redistribute it and/or modify it under
   the terms of the GNU General Public License version 3 as published by the
   Free Software Foundation. This program is distributed in the hope that it
   will be useful, but WITHOUT ANY WARRANTY without even the implied Warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
   Public License for more details. A copy of the GNU General Public License v3
   is available here: <https://www.gnu.org/licenses/>.

   Programmed in Ubuntu-based Linux Platform.
   To run bash script, type in terminal: "sh r.sh"
*******************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

// Call functions in form library & delcare variables
public class CatnMouseui : Form {
  private Label author = new Label();
  private Label speed_label1 = new Label();
  private Label speed_label2 = new Label();
  private TextBox speed_input1 = new TextBox();
  private TextBox speed_input2 = new TextBox();
  private Button start_button = new Button();
  private Label cat_label = new Label();
  private Label mouse_label = new Label();
  private Label distance_label = new Label();
  private TextBox cat_coord = new TextBox();
  private TextBox mouse_coord = new TextBox();
  private TextBox distance = new TextBox();
  private Button quit_button = new Button();
  private Panel header_panel = new Panel();
  private Graphicpanel display_panel = new Graphicpanel();
  private Panel control_panel = new Panel();
  private Size max_exit_ui_size = new Size(1024, 900);
  private Size min_exit_ui_size = new Size(1024, 900);
  // Set up constants and static variables
  private const double refresh_rate = 60.00; // speed in Hz
  private const double motion_clock_rate = 42.60; // speed in tics/seconds
  private static double speed1; // input speed in pixel/seconds
  private static double speed2;
  private static double direction; // hold direction for mouse
  // Statics to update ball locations
  private static double X;
  private static double Y;
  private static double X2;
  private static double Y2;
  private static double ball_center_x;
  private static double ball_center_y;
  private static double ball_center_x2;
  private static double ball_center_y2;
  private static double Δx;
  private static double Δy;
  private static double Δx2;
  private static double Δy2;
  private static double ball_speed_pixel_per_tic1;
  private static double ball_speed_pixel_per_tic2;
  private static double ball_collision; // distance between ball centers
  private static double θ = 0.0; // θ (theta) direction of ball in radians
  private const double turn_angle = Math.PI/12.0; // angle of 30 degrees in radians
  private static bool button_pressed = false; // control start button
  // Declare refresh and ball clock intervals
  private double refresh_clock_interval = 1000.00/refresh_rate;
  private static System.Timers.Timer ui_refresh_clock = new System.Timers.Timer();
  private double ball_clock_interval = 1000.00/motion_clock_rate;
  private static System.Timers.Timer ball_clock = new System.Timers.Timer();
  // Generate random numbers in degrees
  private Random number_creator1 = new Random();
  private Random number_creator2 = new Random();

  // Initialize variables
  public CatnMouseui() {
    // Assign size to the ui
    MaximumSize = max_exit_ui_size;
    MinimumSize = min_exit_ui_size;
    // Initialize string variables
    Text = "The Mouse Chase Game";
    author.Text = "Cat and Mouse by Victor V. Vu";
    speed_label1.Text = "Enter Red Speed (p/s)";
    speed_label2.Text = "Enter White Speed (p/s)";
    start_button.Text = "Start";
    cat_label.Text = "Cat Location";
    mouse_label.Text = "Mouse Location";
    distance_label.Text = "Distance betwen objects";
    quit_button.Text = "Quit";
    // Set size values (width, length)
    author.Size = new Size(450, 40);
    speed_label1.Size = new Size(200, 30);
    speed_label2.Size = new Size(200, 30);
    speed_input1.Size = new Size(70, 60);
    speed_input2.Size = new Size(70, 60);
    start_button.Size = new Size(120, 60);
    cat_label.Size = new Size(150, 30);
    mouse_label.Size = new Size(170, 30);
    distance_label.Size = new Size(225, 30);
    cat_coord.Size = new Size(100, 60);
    mouse_coord.Size = new Size(100, 60);
    distance.Size = new Size(100, 60);
    quit_button.Size = new Size(120, 60);
    header_panel.Size = new Size(1024, 50);
    display_panel.Size = new Size(1024, 625);
    control_panel.Size = new Size(1024, 200);
    // Set color for panel and buttons
    header_panel.BackColor = Color.Cornsilk;
    display_panel.BackColor = Color.BurlyWood;
    control_panel.BackColor = Color.CornflowerBlue;
    start_button.BackColor = Color.MediumAquamarine;
    quit_button.BackColor = Color.MediumAquamarine;
    speed_input1.BackColor = Color.Khaki;
    speed_input2.BackColor = Color.Khaki;
    // Set text fonts and font size
    author.Font = new Font("Times New Roman", 26, FontStyle.Regular);
    speed_label1.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    speed_label2.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    speed_input1.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    speed_input2.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    start_button.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    cat_label.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    mouse_label.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    distance_label.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    cat_coord.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    mouse_coord.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    distance.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    quit_button.Font = new Font("Times New Roman", 15, FontStyle.Regular);
    // Set text alignment and read only status
    author.TextAlign = ContentAlignment.MiddleCenter;
    speed_input1.TextAlign = HorizontalAlignment.Center;
    speed_input2.TextAlign = HorizontalAlignment.Center;
    cat_coord.TextAlign = HorizontalAlignment.Center;
    mouse_coord.TextAlign = HorizontalAlignment.Center;
    distance.TextAlign = HorizontalAlignment.Center;
    cat_coord.ReadOnly = true;
    mouse_coord.ReadOnly = true;
    distance.ReadOnly = true;
    // Set locations (width, length)
    author.Location = new Point(300, 5);
    speed_label1.Location = new Point(200, 25);
    speed_label2.Location = new Point(410, 25);
    speed_input1.Location = new Point(240, 60);
    speed_input2.Location = new Point(450, 60);
    start_button.Location = new Point(50, 75);
    cat_label.Location = new Point(215, 100);
    mouse_label.Location = new Point(405, 100);
    distance_label.Location = new Point(620, 60);
    cat_coord.Location = new Point(220, 130);
    mouse_coord.Location = new Point(420, 130);
    distance.Location = new Point(670, 90);
    quit_button.Location = new Point(850, 75);
    header_panel.Location = new Point(0, 0);
    display_panel.Location = new Point(0, 50);
    control_panel.Location = new Point(0, 675);
    // Control elements to display
    Controls.Add(header_panel);
    header_panel.Controls.Add(author);
    Controls.Add(display_panel);
    Controls.Add(control_panel);
    control_panel.Controls.Add(speed_label1);
    control_panel.Controls.Add(speed_label2);
    control_panel.Controls.Add(speed_input1);
    control_panel.Controls.Add(speed_input2);
    control_panel.Controls.Add(start_button);
    control_panel.Controls.Add(cat_label);
    control_panel.Controls.Add(mouse_label);
    control_panel.Controls.Add(distance_label);
    control_panel.Controls.Add(cat_coord);
    control_panel.Controls.Add(mouse_coord);
    control_panel.Controls.Add(distance);
    control_panel.Controls.Add(quit_button);
    // Control buttons when are clicked
    start_button.Click += new EventHandler(start);
    quit_button.Click += new EventHandler(terminate);
    // Set properties of the refresh and ball clock
    ui_refresh_clock.Enabled = false;
    ui_refresh_clock.Interval = refresh_clock_interval;
    ui_refresh_clock.Elapsed += new ElapsedEventHandler(refresh_ui);
    ball_clock.Enabled = false;
    ball_clock.Interval = ball_clock_interval;
    ball_clock.Elapsed += new ElapsedEventHandler(update_ball_coords);
    // Set location to start at 1/3 of width
    X = display_panel.Width / 3;
    Y = display_panel.Height / 2;
    X2 = display_panel.Width - display_panel.Width / 3;
    Y2 = display_panel.Height / 2;
    // Allow ball center to control coords
    ball_center_x = X;
    ball_center_y = Y;
    ball_center_x2 = X2;
    ball_center_y2 = Y2;
    // Locations & Distance are displayed before start
    cat_coord.Text = "(" + (int)Math.Round(X) + ", " + (int)Math.Round(Y) + ")";
    mouse_coord.Text = "(" + (int)Math.Round(X2) + ", " + (int)Math.Round(Y2) + ")";
    ball_collision = Math.Sqrt(Math.Pow((ball_center_x - ball_center_x2), 2) +
                               Math.Pow((ball_center_y - ball_center_y2), 2));
    distance.Text = "" + ball_collision + "";

    CenterToScreen(); // center screen when opened
  } // End of ui constructor

  // Function to start animation & perform computations
  protected void start(Object sender, EventArgs h) {

    try { // check if user inputted coords
      if ((speed_input1 ?? speed_input2) != null) {
        // convert input to double
        speed1 = double.Parse(speed_input1.Text);
        speed2 = double.Parse(speed_input2.Text);
        // control the speed
        ball_speed_pixel_per_tic1 = speed1 / motion_clock_rate;
        ball_speed_pixel_per_tic2 = speed2 / motion_clock_rate;
        // generate numbers between 0-360 degrees
        θ = number_creator1.NextDouble()*2.0*Math.PI; // cat direction
        direction = number_creator2.NextDouble() * (360-0) + (0); // mouse direction
        // convert degrees to radians
        Δx = (ball_speed_pixel_per_tic1)*Math.Cos(θ);
        Δy = (ball_speed_pixel_per_tic1)*-Math.Sin(θ);
        Δx2 = (ball_speed_pixel_per_tic2)*Math.Cos(((Math.PI / 180) * -direction));
        Δy2 = (ball_speed_pixel_per_tic2)*Math.Sin(((Math.PI / 180) * -direction));
        display_panel.Focus(); // call OnKeyDown to detect input
      } // end of if statement
    } // end of try
    catch (Exception) { // prevents program from crashing in case of error
      Console.WriteLine("No input detected"); // program does nothing
    } // end of catch
    if (button_pressed == false) { // begin timers
      start_button.Text = "Pause";
      button_pressed = true;
      ui_refresh_clock.Enabled = true;
      ball_clock.Enabled = true;
    } else { // pause timers
      start_button.Text = "Resume";
      button_pressed = false;
      ui_refresh_clock.Enabled = false;
      ball_clock.Enabled = false;
    }
  } // End of method initialize

  // Function to update coords & animate the ball
  protected void update_ball_coords(System.Object sender, ElapsedEventArgs even) {
    // check if the balls have collided with walls
    ball_center_x += Δx;
    ball_center_y += Δy;
    // collision checks for ball 1
    if (ball_center_x + 25 >= 1015 || ball_center_x - 25 <= 0) {
      Δx = -1 * Δx;
    }
    if (ball_center_y + 25 >= display_panel.Height || ball_center_y - 25 <= 0) {
      Δy = -1 * Δy;
    }
    ball_center_x2 += Δx2;
    ball_center_y2 += Δy2;
    // collision checks for ball 2
    if (ball_center_x2 + 15 >= 1015 || ball_center_x2 - 15 <= 0) {
      Δx2 = -1 * Δx2;
    }
    if (ball_center_y2 + 15 >= display_panel.Height || ball_center_y2 - 15 <= 0) {
      Δy2 = -1 * Δy2;
    }
    // checks if the two balls collided with each other
    ball_collision = Math.Sqrt(Math.Pow((ball_center_x - ball_center_x2), 2) +
                               Math.Pow((ball_center_y - ball_center_y2), 2));

    if (ball_collision <= 25 + 15) { // collision if distance is smaller than radius
      Console.WriteLine("The Mouse has been caught."); // collision has been detected
      // Game Over, stop all timers
      ui_refresh_clock.Enabled = false;
      ball_clock.Enabled = false;
    }
  } // End of method update_ball_coords

  // Tracks the current locations & distance
  protected void refresh_ui(Object sender, EventArgs h) {
    cat_coord.Text = "(" + (int)Math.Round(ball_center_x) + ", " + (int)Math.Round(ball_center_y) + ")";
    mouse_coord.Text = "(" + (int)Math.Round(ball_center_x2) + ", " + (int)Math.Round(ball_center_y2) + ")";
    distance.Text = "" + (int)Math.Round(ball_collision) + "";
    display_panel.Invalidate(); // calls OnPaint
  }

  // Function called by quit button to terminate
  protected void terminate(Object sender, EventArgs h) {
    Console.WriteLine("This program will now quit.");
    Close();
  }

  // Graphic class to output panels
  public class Graphicpanel : Panel {
    public Graphicpanel() { Console.WriteLine("A graphic panel was created."); }
    // Calls OnPaint to draw objects
    protected override void OnPaint(PaintEventArgs ii) {
      Graphics graph = ii.Graphics;
      // (x, y, width, length)
      graph.FillEllipse(Brushes.Crimson, (float)Math.Round(ball_center_x - 25),
                        (float)Math.Round(ball_center_y - 25), 50, 50);
      graph.FillEllipse(Brushes.White, (float)Math.Round(ball_center_x2 - 15),
                        (float)Math.Round(ball_center_y2 - 15), 30, 30);
      base.OnPaint(ii);
    } // OnPaint end
    // Function to detect key presses
    protected override void OnKeyDown(KeyEventArgs e) {
      if (e.KeyCode == Keys.Left) {
        θ -= turn_angle; // turn 30 deg clockwise
        Δx = (ball_speed_pixel_per_tic1)*Math.Cos(θ);
        Δy = (ball_speed_pixel_per_tic1)*-Math.Sin(θ);
      }
      if (e.KeyCode == Keys.Right) {
        θ += turn_angle; // turn 30 deg counter-clockwise
        Δx = (ball_speed_pixel_per_tic1)*Math.Cos(θ);
        Δy = (ball_speed_pixel_per_tic1)*-Math.Sin(θ);
      }
      base.OnKeyDown(e);
    } // End of OnKeyDown
  } // End of graphics constructor
} // End of main class
