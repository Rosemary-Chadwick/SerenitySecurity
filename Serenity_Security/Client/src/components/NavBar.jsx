import React, { useState } from 'react';
import { Link, NavLink as RRNavLink } from 'react-router-dom';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  Container
} from 'reactstrap';
import { useTheme } from './theme/ThemeContext';
import Logo from './common/Logo';
import ThemeToggle from './common/ThemeToggle';

export const NavBar = ({ loggedInUser, setLoggedInUser }) => {
  const [isOpen, setIsOpen] = useState(false);
  const { colors } = useTheme();
  
  const toggle = () => setIsOpen(!isOpen);

  return (
<Navbar
  expand="md"
  style={{
    backgroundColor: '#0a192f',
    boxShadow: '0 2px 10px rgba(0, 0, 0, 0.2)',
    padding: '0.25rem 1rem',
    height: '50px' // Explicitly set a fixed height
  }}
  dark
>
  <Container className="d-flex align-items-center">
    {/* Logo and Title */}
    <NavbarBrand tag={Link} to="/" className="me-auto">
      <div className="d-flex align-items-center">
        <Logo width={32} height={32} />
        <span className="ms-2 fw-bold" style={{ color: colors.secondary }}>
          Serenity Shield
        </span>
      </div>
    </NavbarBrand>
        
        <NavbarToggler onClick={toggle} />
        
        <Collapse isOpen={isOpen} navbar>
          <Nav className="me-auto" navbar>
            {/* Left side is empty - no navigation links as requested */}
          </Nav>
          
          <Nav className="ms-auto d-flex align-items-center" navbar>
            <NavItem className="me-3">
              <ThemeToggle />
            </NavItem>
            
            {loggedInUser ? (
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/profile">
                    Profile
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink
                    tag={Link}
                    to="/login"
                    onClick={() => setLoggedInUser(null)}
                  >
                    Logout
                  </NavLink>
                </NavItem>
              </>
            ) : (
              <>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/login">
                    Login
                  </NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={RRNavLink} to="/register">
                    Register
                  </NavLink>
                </NavItem>
              </>
            )}
          </Nav>
        </Collapse>
      </Container>
    </Navbar>
  );
};