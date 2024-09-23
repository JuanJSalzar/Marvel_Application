<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Characters.aspx.cs" Inherits="Marvel_Application.Views.Characters" Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Marvel Characters</title>
    <link rel="stylesheet" type="text/css" href="../Assets/site.css" />
    <link rel="preconnect" href="https://fonts.googleapis.com"/>
    <link href="https://fonts.googleapis.com/css2?family=Roboto&display=swap" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css2?family=Bangers&display=swap" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">

        <!-- Header -->
        <header class="site-header">
            <nav class="site-nav">
                <h1 class="site-title">
                    <a href="Characters.aspx">Marvel Application</a>
                </h1>
            </nav>
        </header>
        <div class="main-content">
            <div class="search-container">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="search-box" Placeholder="Search name" AutoPostBack="true" OnTextChanged="PerformSearch"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="PerformSearch" CssClass="search-button" />
            </div>
            <div class="cards-container">
                <asp:Repeater ID="rptCharacters" runat="server">
                    <ItemTemplate>
                        <div class="card">
                            <img
                                id='<%# Eval("Id") %>'
                                src='<%# GetImageUrl(Eval("Thumbnail")) %>' 
                                alt='<%# Eval("Name") %>' 
                                class="character-image"
                            />
                            <h2 id='<%# Eval("Id") + "_name" %>'><%# Eval("Name") %></h2>
                            <p id='<%# Eval("Id") + "_desc" %>'><%# GetDescription(Eval("Description")) %></p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Label ID="lblNoData" runat="server" CssClass="no-data" Text="No characters found." Visible="false"></asp:Label>
            </div>
            <div class="pagination">
                <asp:Button ID="btnPrev" runat="server" Text="Back" OnClick="BtnPrev_Click" CssClass="pagination-button" />
                <asp:Label ID="lblPageInfo" runat="server" CssClass="page-info"></asp:Label>
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="BtnNext_Click" CssClass="pagination-button" />
            </div>
        </div>

        <!-- Modal -->
        <div id="characterModal" class="modal" role="dialog" aria-labelledby="modalName" aria-modal="true">
            <div class="modal-content">
                <span class="close-button" aria-label="Close">&times;</span>
                <img id="modalImage" src="" alt="Character image" class="modal-image" />
                <h2 id="modalName"></h2>
                <div id="modalDescription"></div>
            </div>
        </div>

        <!-- Footer -->
        <footer class="site-footer">
            <p>Technical Test by Juan José Salazar - 2024 Reserved rights©</p>
        </footer>

        <!-- JavaScript -->
        <script type="text/javascript">
            document.addEventListener('DOMContentLoaded', function () {
                var modal = document.getElementById('characterModal');

                var modalImage = document.getElementById('modalImage');
                var modalName = document.getElementById('modalName');
                var modalDescription = document.getElementById('modalDescription');
                var closeButton = document.querySelector('.close-button');

                var characterImages = document.querySelectorAll('.character-image');

                var body = document.body;

                var focusedElementBeforeModal = null;

                function openModal(name, description, imageSrc) {
                    focusedElementBeforeModal = document.activeElement;

                    modalImage.src = imageSrc;
                    modalImage.alt = name;
                    modalName.textContent = name;
                    modalDescription.textContent = description;

                    modal.classList.add('show');

                    body.classList.add('no-scroll');

                    closeButton.focus();
                }

                function closeModal() {
                    modal.classList.remove('show');

                    setTimeout(function () {
                        body.classList.remove('no-scroll');

                        if (focusedElementBeforeModal) {
                            focusedElementBeforeModal.focus();
                        }
                    }, 300); 
                }

                characterImages.forEach(function (img) {
                    img.addEventListener('click', function () {
                        const id = this.getAttribute('id');
                        const description = document.getElementById(id + '_desc').textContent;
                        const name = document.getElementById(id + '_name').textContent;
                        const imageSrc = this.getAttribute('src');

                        openModal(name, description, imageSrc);
                    });
                });

                closeButton.addEventListener('click', function () {
                    closeModal();
                });

                window.addEventListener('click', function (event) {
                    if (event.target == modal) {
                        closeModal();
                    }
                });

                window.addEventListener('keydown', function (event) {
                    if (event.key === 'Escape') {
                        closeModal();
                    }
                });

                modal.addEventListener('transitionend', function () {
                    if (modal.classList.contains('hide')) {
                        modal.style.display = 'none';
                    } else if (modal.classList.contains('show')) {
                        modal.style.display = 'block';
                    }
                });
            });
        </script>
    </form>
</body>
</html>