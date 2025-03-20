# Serenity_Security Client

This is the React frontend for the Serenity_Security application.

## Getting Started

To run the development server:

```
npm run dev
```

To build for production:

```
npm run build
```

## Data Visualization with Recharts

### Overview

Serenity Shield utilizes Recharts, a composable charting library built on React components, to visualize security metrics and vulnerability data. Based on D3.js, Recharts provides a declarative API that seamlessly integrates with our React application while maintaining high rendering performance.

### Implementation Details

The application implements several chart types to display security information:

- **Donut Charts**: Used on asset cards to show vulnerability severity distribution with customized color schemes that align with our application theme.
- **Security Metrics Panel**: Features multiple chart types to provide comprehensive security insights:
  - Vulnerability severity distribution charts
  - Remediation status visualization
  - Asset vulnerability comparison using stacked bar charts

### Customizations

All charts have been customized to match Serenity Shield's design language:

- **Color Palette**: Charts use muted colors that complement our navy and cream theme:
  - High severity: #a83246 (muted red)
  - Medium severity: #d9b55a (muted yellow)
  - Low severity: #5a9178 (muted green)
- **Tooltips**: Enhanced with cream backgrounds and navy text for better readability
- **Responsiveness**: All charts are wrapped in ResponsiveContainer for proper scaling across different device sizes

### Modifying Charts

To modify existing charts or add new visualizations:

1. Chart components are located in `src/components/charts/` directory
2. Dashboard metrics are configured in `src/components/dashboard/DashboardCharts.jsx`
3. Asset card visualizations are integrated within `src/components/assets/AssetCard.jsx`

To change chart colors or styling, modify the relevant component's color constants or style objects. For significant layout changes, adjust the ResponsiveContainer dimensions and internal chart parameters.

### Dependencies

```json
"dependencies": {
  "recharts": "^2.5.0",
  // other dependencies
}
```

deletion uses a Reactstrap Modal component with custom styling

External Libraries and Components Used in the Serenity Security Application
Reactstrap Components
Reactstrap provides React-based Bootstrap 4 components. The following components have been incorporated into the application:

Modal Components

Modal - Container for modal dialogs
ModalHeader - Styled header section for modals
ModalBody - Content area for modals
ModalFooter - Footer section for modal action buttons
Used for confirmation dialogs (report/asset deletion)

Card Components

Card - Main container for content sections
CardHeader - Header section for cards
CardBody - Content area for cards
CardTitle - Styled title element for card headers
Used throughout the application for organizing content

Form Components

Button - Styled button element
Input - Form input element
FormGroup - Container for form elements
Label - Form field label
FormFeedback - Validation feedback messages
Used in login, registration, and asset forms

React Router Components
React Router is used for navigation and routing in the application:

Routing Components

Routes, Route - Define application routes
Link - Navigation without page refresh
useNavigate - Programmatic navigation hook
useParams - Access URL parameters
useLocation - Access query string parameters

Bootstrap Styling Classes
Bootstrap CSS classes are used extensively for styling:

Layout

Grid system (container, row, col-md-\*)
Spacing utilities (mt-4, mb-3, p-2, etc.)
Flex utilities (d-flex, justify-content-between, etc.)

Components

Alert styles (alert, alert-danger, alert-success, etc.)
Table styles (table, table-striped)
List groups (list-group, list-group-item)
Progress bars (progress, progress-bar)
Badges (badge, custom-styled badges)
Form controls (form-check, form-switch, etc.)

Recharts Library
Recharts is used for data visualization:

Chart Components

PieChart, Pie, Cell - Building blocks for the donut chart
ResponsiveContainer - Makes charts responsive
Tooltip - Shows data on hover
Legend - Chart legend
Used for vulnerability severity distribution visualization

SVG Icons
The application uses SVG-based icons:

Custom SVG Icons

Trash icon for deletion actions
Sun/Moon icons for theme toggle
Used for intuitive visual cues in the UI

Context API
React's Context API is used for application-wide state management:

Custom Contexts

ThemeContext - Manages light/dark theme
useTheme hook - Access theme properties across components

These components and libraries work together to create a cohesive, responsive, and visually consistent security vulnerability management application with a custom theme system.
