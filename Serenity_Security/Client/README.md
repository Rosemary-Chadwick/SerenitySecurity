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
