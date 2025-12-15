document.addEventListener("DOMContentLoaded", () => {
  const year = document.getElementById("year");
  year.textContent = new Date().getFullYear();
});

// Smooth scroll to sections centered vertically
document.querySelectorAll('nav a[href^="#"]').forEach(link => {
  link.addEventListener("click", function(e) {
    e.preventDefault(); // stop default jump

    const targetId = this.getAttribute("href").substring(1);
    const target = document.getElementById(targetId);

    // If section exists
    if (target) {
      // Calculate offset so section appears centered
      const offset = window.innerHeight / 2 - target.offsetHeight / 2;

      // Scroll to that position smoothly
      window.scrollTo({
        top: target.offsetTop - offset,
        behavior: "smooth"
      });
    }
  });
});

