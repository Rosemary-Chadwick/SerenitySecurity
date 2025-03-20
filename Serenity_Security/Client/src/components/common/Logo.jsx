import React from 'react';
import { useTheme } from '../theme/ThemeContext';

const Logo = ({ width = 40, height = 40 }) => {
  const { isDarkMode } = useTheme();

  return (
    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 200 200" width={width} height={height}>
      {/* Shield Base */}
      <path
        d="M100 20 L160 40 C160 100, 140 160, 100 180 C60 160, 40 100, 40 40 Z"
        fill={isDarkMode ? "#1b2a3c" : "#ffd365"}
        stroke={isDarkMode ? "#ffd365" : "#1b2a3c"}
        strokeWidth="4"
      />

      {/* Lock Body */}
      <rect
        x="70"
        y="80"
        width="60"
        height="50"
        rx="5"
        ry="5"
        fill={isDarkMode ? "#ffd365" : "#1b2a3c"}
      />

      {/* Lock Shackle */}
      <path
        d="M85 80 C85 60, 115 60, 115 80"
        fill="none"
        stroke={isDarkMode ? "#ffd365" : "#1b2a3c"}
        strokeWidth="12"
        strokeLinecap="round"
      />

      {/* Keyhole */}
      <circle cx="100" cy="100" r="8" fill={isDarkMode ? "#1b2a3c" : "#ffd365"} />
      <rect
        x="98"
        y="100"
        width="4"
        height="15"
        fill={isDarkMode ? "#1b2a3c" : "#ffd365"}
      />
    </svg>
  );
};

export default Logo;
